using LinkedIn.Models.Posts;

namespace LinkedIn.Services.IServices
{
    public interface IPostsService
    {
        Task<IEnumerable<UserPost>> GetAll(CancellationToken cancellationToken);
        Task<UserPost> GetById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<UserPost>> GetAllConnectionsPosts(int userId, CancellationToken cancellationToken);
        Task<UserPost> Create(PostCreateRequest createRequest, CancellationToken cancellationToken);
        Task<UserPost> Update(PostUpdateRequest updateRequest, CancellationToken cancellationToken);
        Task<UserPost> ReactOnPost(PostReactionModel reactionModel, CancellationToken cancellationToken);
        Task<bool> Delete(int postId, CancellationToken cancellationToken);
        Task<Comment> CommentOnPost(CommentCreateRequest createRequest, CancellationToken cancellationToken);
        Task<CommentReactions> ReactOnComment(CommentReactionCreateRequest createReqeust, CancellationToken cancellationToken);
    }
}
