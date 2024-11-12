using AutoMapper;
using LinkedIn.ConnectionWebSocketHandler;
using LinkedIn.Data;
using LinkedIn.Models.Posts;
using LinkedIn.Models.Users;
using LinkedIn.Repository.IRepository;
using LinkedIn.Services.IServices;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.OpenApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LinkedIn.Services
{
    public class PostsService : IPostsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly WebSocketHandler _websocketHandler;
        public IUserService _userService;
        private readonly IDataContext _dataContext;

        public PostsService(IUnitOfWork unitOfWork, WebSocketHandler webSocketHandler, IUserService userService, IDataContext dataContext)
        {   
            _unitOfWork = unitOfWork;
            _websocketHandler = webSocketHandler;
            _userService = userService;
            _dataContext = dataContext;
        }

        public async Task<UserPost> GetById(int id, CancellationToken cancellationToken)
        {
            var postFromDb = await _unitOfWork.UserPosts.GetById(id, cancellationToken);

            if (postFromDb == null)
            {
                throw new Exception("Post with the given id " + id + " was not found!");
            }

            return postFromDb;
        }

        public async Task<IEnumerable<UserPost>> GetAll(CancellationToken cancellationToken)
        {
            var postsFromDb = await _unitOfWork.UserPosts.GetAll(cancellationToken);

            if (postsFromDb == null)
            {
                return [];
            }

            return postsFromDb;
        }

        public async Task<IEnumerable<UserPost>> GetAllConnectionsPosts(int userId, CancellationToken cancellationToken)
        {
            var userConnectionsFromDb = await _unitOfWork.Connections.GetAllAcceptedConnectionsWithSenderAndReceiver(userId, cancellationToken);
            var userPostsFromDb = await _unitOfWork.UserPosts.GetAllWithUser(cancellationToken);

            var reactionsFromDb = await _unitOfWork.Reactions.GetAllWithType(cancellationToken);
            var commentsFromDb = await _unitOfWork.Comments.GetAllWithUser(cancellationToken);

            var userConnectionsIds = new List<int>();

            foreach(var connection in userConnectionsFromDb)
            {
                if(connection.SenderId != userId)
                {
                    userConnectionsIds.Add(connection.SenderId);
                } 
                else if(connection.ReceiverId != userId)
                {
                    userConnectionsIds.Add(connection.ReceiverId);
                }
            }

            var connectionsPosts = userPostsFromDb.Where(userPost => userConnectionsIds.Any(id => id == userPost.PosterId)).ToList();

            if(connectionsPosts == null)
            {
                throw new Exception("Your connections have no posts!");
            }

            return connectionsPosts;
        }

        public async Task<IEnumerable<UserPost>> GetAllConnectionsAndUserPosts(int userId, CancellationToken cancellationToken)
        {
            var userConnectionsFromDb = await _unitOfWork.Connections.GetAllAcceptedConnectionsWithSenderAndReceiver(userId, cancellationToken);
            var userPostsFromDb = await _unitOfWork.UserPosts.GetAllWithUser(cancellationToken);

            var reactionsFromDb = await _unitOfWork.Reactions.GetAllWithType(cancellationToken);
            var commentsFromDb = await _unitOfWork.Comments.GetAllWithUser(cancellationToken);

            var userConnectionsIds = new List<int>();

            foreach (var connection in userConnectionsFromDb)
            {
                if (connection.SenderId != userId)
                {
                    userConnectionsIds.Add(connection.SenderId);
                }
                else if (connection.ReceiverId != userId)
                {
                    userConnectionsIds.Add(connection.ReceiverId);
                }
            }

            userConnectionsIds.Add(userId);

            var connectionsPosts = userPostsFromDb.Where(userPost => userConnectionsIds.Any(id => id == userPost.PosterId)).ToList();

            if (connectionsPosts == null)
            {
                throw new Exception("Your connections have no posts!");
            }

            return connectionsPosts;
        }

        public async Task<UserPost> Create(PostCreateRequest createRequest, CancellationToken cancellationToken)
        { 
            var newPost = new UserPost
            {
                PosterId = createRequest.UserId,
                Content = createRequest.Content,
                PostImage = createRequest.Image,
                TimePosted = DateTime.UtcNow.ToUniversalTime(),
            };

            _unitOfWork.UserPosts.Add(newPost);
            _dataContext.LogTracker();
            try
            {
                await _unitOfWork.SaveChangesAsync();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var userFromDb = await _userService.GetById(createRequest.UserId, cancellationToken);

            if (userFromDb.Connections == null)
            {
                return newPost;
            }

            var notificationTasks = new List<Task>();

            foreach (var connection in userFromDb.Connections)
            {
                notificationTasks.Add(_websocketHandler.NotifyUserOfNewEvent(connection.Id, newPost, "post"));
            }

            await Task.WhenAll(notificationTasks);

            return newPost;
        }

        public async Task<UserPost> Update(PostUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            var postFromDb = await _unitOfWork.UserPosts.GetById(updateRequest.PostId, cancellationToken);

            postFromDb.Content = updateRequest.Content;
            postFromDb.PostImage = updateRequest.Image;
            postFromDb.IsEdited = true;

            await _unitOfWork.SaveChangesAsync();

            return postFromDb;
        }

        public async Task<UserPost> ReactOnPost(PostReactionModel reactionModel, CancellationToken cancellationToken)
        {
            var postFromDb = await _unitOfWork.UserPosts.GetById(reactionModel.PostId, cancellationToken);
            var reactionTypeFromDb = await _unitOfWork.ReactionTypes.GetByName(reactionModel.ReactionType, cancellationToken);

            if(postFromDb == null)
            {
                throw new Exception("Post with the given id " + reactionModel.PostId + " was not found!");
            }

            var reactionFromDb = await _unitOfWork.Reactions.GetReactionsForUserPost(reactionModel.PostId, cancellationToken);
            var existingReaction = reactionFromDb.Where(reaction => reaction.UserId == reactionModel.UserId).FirstOrDefault();

            if(existingReaction != null)
            {
                if(existingReaction.Type.Name != reactionModel.ReactionType)
                {
                    existingReaction.Type = reactionTypeFromDb;
                }
                else 
                { 
                    _unitOfWork.Reactions.Remove(existingReaction);
                    postFromDb.NumberOfReactions--;
                }

                await _unitOfWork.SaveChangesAsync();
                return postFromDb;
            }

            var newPostReaction = new Reaction
            {
                PostId = postFromDb.Id,
                TimeReacted = DateTime.UtcNow.ToUniversalTime(),
                UserId = reactionModel.UserId,
                Type = reactionTypeFromDb,
            };

            _unitOfWork.Reactions.Add(newPostReaction);

            postFromDb.NumberOfReactions++;

            await _unitOfWork.SaveChangesAsync();

            return postFromDb;
        }

        public async Task<bool> Delete(int postId, CancellationToken cancellationToken)
        {
            var postFromDb = await _unitOfWork.UserPosts.GetById(postId, cancellationToken);

            if(postFromDb == null)
            {
                throw new Exception("Post with the given id " + postId + " was not found!");
            }

            _unitOfWork.UserPosts.Remove(postFromDb);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<Comment> CommentOnPost(CommentCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(createRequest.UserId, cancellationToken);
            var postFromDb = await _unitOfWork.UserPosts.GetById(createRequest.PostId, cancellationToken);

            if (userFromDb == null) 
            {
                throw new Exception("User with the given id " + createRequest.UserId + " was not found!");
            }

            var connectionsToBeNotified = await _unitOfWork.Connections.GetAllAcceptedConnectionsWithSenderAndReceiver(postFromDb.PosterId, cancellationToken);
            var connectionsIdsToBeNotified = new List<int>();

            if(postFromDb == null)
            {
                throw new Exception("Post with the given id " + createRequest.PostId + " was not found!");
            }

            var newComment = new Comment
            {
                PostId = createRequest.PostId,
                UserId = createRequest.UserId,
                Content = createRequest.Content,
                TimeCommented = DateTime.UtcNow.ToUniversalTime(),
            };

            postFromDb.NumberOfComments++;

            _unitOfWork.Comments.Add(newComment);
            await _unitOfWork.SaveChangesAsync();

            var notificationTasks = new List<Task>();

            foreach (var connection in connectionsToBeNotified)
            {
                connectionsIdsToBeNotified.Add(connection.SenderId);
                connectionsIdsToBeNotified.Add(connection.ReceiverId);
            }

            var uniqueConnections = connectionsIdsToBeNotified.Distinct();

            foreach (var userId in uniqueConnections)
            {
                notificationTasks.Add(_websocketHandler.NotifyUserOfNewEvent(userId, new CommentDTO
                {
                    Id = newComment.Id,
                    PostId = newComment.PostId,
                    Content = newComment.Content,
                    TimeCommented = newComment.TimeCommented,
                    UserPost = postFromDb,
                    User = new UserDTO
                    {
                        Id = userFromDb.Id,
                        FirstName = userFromDb.FirstName,
                        LastName = userFromDb.LastName,
                        ImageUrl = userFromDb.ImageUrl,
                        Job = userFromDb.Job,
                    }
                }, "comment"));
            }

            await Task.WhenAll(notificationTasks);

            return newComment;
        }

        public async Task<CommentReactions> ReactOnComment(CommentReactionCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var reactionTypeFromDb = await _unitOfWork.ReactionTypes.GetByName(createRequest.ReactionType, cancellationToken);
            var postFromDb = await _unitOfWork.UserPosts.GetById(createRequest.PostId, cancellationToken);
            var commentFromDb = await _unitOfWork.Comments.GetById(createRequest.CommentId, cancellationToken);

            var existingReaction = commentFromDb.Reactions.Find(reaction => reaction.UserId == createRequest.UserId);

            if(existingReaction != null)
            {
                if(existingReaction.ReactionType.Name == createRequest.ReactionType)
                {
                    _unitOfWork.CommentReactions.Remove(existingReaction);
                }
                else
                {
                    existingReaction.ReactionType = reactionTypeFromDb;
                }

                var connectionsToBeNotified2 = await _unitOfWork.Connections.GetAllAcceptedConnectionsWithSenderAndReceiver(postFromDb.PosterId, cancellationToken);
                var connectionsIdsToBeNotified2 = new List<int>();

                foreach (var connection in connectionsToBeNotified2)
                {
                    connectionsIdsToBeNotified2.Add(connection.SenderId);
                    connectionsIdsToBeNotified2.Add(connection.ReceiverId);
                }

                var uniqueConnections2 = connectionsIdsToBeNotified2.Distinct();

                var notificationTasks2 = new List<Task>();

                foreach (var userId in uniqueConnections2)
                {
                    notificationTasks2.Add(_websocketHandler.NotifyUserOfNewEvent(userId, existingReaction, "commentReaction"));
                }

                await Task.WhenAll(notificationTasks2);

                await _unitOfWork.SaveChangesAsync();

                return existingReaction;
            }

            var newCommentReaction = new CommentReactions
            {
                CommentId = createRequest.CommentId,
                ReactionTypeId = reactionTypeFromDb.Id,
                UserId = createRequest.UserId,
            };

            _unitOfWork.CommentReactions.Add(newCommentReaction);
            await _unitOfWork.SaveChangesAsync();

            var connectionsToBeNotified = await _unitOfWork.Connections.GetAllAcceptedConnectionsWithSenderAndReceiver(postFromDb.PosterId, cancellationToken);
            var connectionsIdsToBeNotified = new List<int>();

            foreach (var connection in connectionsToBeNotified)
            {
                connectionsIdsToBeNotified.Add(connection.SenderId);
                connectionsIdsToBeNotified.Add(connection.ReceiverId);
            }

            var uniqueConnections = connectionsIdsToBeNotified.Distinct();

            var notificationTasks = new List<Task>();

            foreach (var userId in uniqueConnections)
            {
                notificationTasks.Add(_websocketHandler.NotifyUserOfNewEvent(userId, newCommentReaction, "commentReaction"));
            }

            await Task.WhenAll(notificationTasks);

            return newCommentReaction;
        }
    }
}
