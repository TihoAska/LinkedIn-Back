using LinkedIn.Data;
using LinkedIn.Models.Posts;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Repository
{
    public class ReactionTypesRepository : GenericRepository<ReactionType>, IReactionTypesRepository
    {
        public ReactionTypesRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task<ReactionType> GetByName(string name, CancellationToken cancellationToken)
        {
            return await _query.Where(reactionType => reactionType.Name == name).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
