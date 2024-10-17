using LinkedIn.Data;
using LinkedIn.Models.Posts;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Repository
{
    public class ReactionsRepository : GenericRepository<Reaction>, IReactionsRepository
    {
        public ReactionsRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task<IEnumerable<Reaction>> GetReactionsForUserPost(int userPostId, CancellationToken cancellationToken)
        {
            return await _query.Where(reaction => reaction.PostId == userPostId)
                .Include(reaction => reaction.User)
                .Include(reaction => reaction.Type)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Reaction>> GetAllWithType(CancellationToken cancellationToken)
        {
            return await _query
                .Include(reaction => reaction.Type)
                .ToListAsync(cancellationToken);
        }
    }
}
