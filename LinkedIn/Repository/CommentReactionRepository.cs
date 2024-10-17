using LinkedIn.Data;
using LinkedIn.Models.Posts;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LinkedIn.Repository
{
    public class CommentReactionRepository : GenericRepository<CommentReactions>, ICommentReactionsRepository
    {
        public CommentReactionRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task<IEnumerable<CommentReactions>> GetAllByCommentId(int commentId, CancellationToken cancellationToken)
        {
            return await _query.Where(commentReaction => commentReaction.CommentId == commentId).ToListAsync(cancellationToken);
        }
    }
}
