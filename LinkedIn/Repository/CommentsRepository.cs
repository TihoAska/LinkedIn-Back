using LinkedIn.Data;
using LinkedIn.Models.Posts;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Repository
{
    public class CommentsRepository : GenericRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task<IEnumerable<Comment>> GetAllWithUser(CancellationToken cancellationToken)
        {
            return await _query
                .Include(comment => comment.Reactions)
                    .ThenInclude(reaction => reaction.ReactionType)
                .Include(comment => comment.User)
                .ToListAsync();
        }

        public async Task<Comment> GetById(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(comment => comment.Id == id)
                .Include(comment => comment.Reactions)
                    .ThenInclude(reaction => reaction.ReactionType)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
