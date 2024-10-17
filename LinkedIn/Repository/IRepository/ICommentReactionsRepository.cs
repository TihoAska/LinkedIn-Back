using LinkedIn.Models.Posts;

namespace LinkedIn.Repository.IRepository
{
    public interface ICommentReactionsRepository : IGenericRepository<CommentReactions>
    {
        Task<IEnumerable<CommentReactions>> GetAllByCommentId(int commentId, CancellationToken cancellationToken);
    }
}
