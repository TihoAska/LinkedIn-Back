using LinkedIn.ConnectionWebSocketHandler;
using LinkedIn.Data;
using LinkedIn.Models.Posts;
using LinkedIn.Models.Users;
using LinkedIn.Repository.IRepository;
using LinkedIn.Services.IServices;
using Microsoft.EntityFrameworkCore.Storage.Json;

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
            var userPostsFromDb = await _unitOfWork.UserPosts.GetAll(cancellationToken);

            var reactionsFromDb = await _unitOfWork.Reactions.GetAll(cancellationToken);
            var commentsFromDb = await _unitOfWork.Comments.GetAll(cancellationToken);

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

            var connectionsPosts = userPostsFromDb.Where(userPost => userConnectionsIds.Any(id => id == userPost.PosterId));

            if(connectionsPosts == null)
            {
                throw new Exception("Your connections have no posts!");
            }

            foreach(var connectionPost in connectionsPosts)
            {
                var reactionsForPost = reactionsFromDb.Where(reactions => reactions.PostId == connectionPost.Id);
                var commentsForPost = commentsFromDb.Where(comments => comments.PostId == connectionPost.Id);

                if(connectionPost.Comments == null)
                {
                    connectionPost.Comments = new List<Comment>();
                }

                if(connectionPost.Reactions == null)
                {
                    connectionPost.Reactions = new List<Reaction>();
                }

                foreach(var reaction in reactionsForPost)
                {
                    connectionPost.Reactions.Add(reaction);
                }

                foreach(var comment in commentsForPost)
                {
                    connectionPost.Comments.Add(comment);
                }
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
    }
}
