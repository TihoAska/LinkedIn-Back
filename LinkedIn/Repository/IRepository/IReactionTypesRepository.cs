using LinkedIn.Models.Posts;

namespace LinkedIn.Repository.IRepository
{
    public interface IReactionTypesRepository : IGenericRepository<ReactionType>
    {
        Task<ReactionType> GetByName(string name, CancellationToken cancellationToken);
    }
}
