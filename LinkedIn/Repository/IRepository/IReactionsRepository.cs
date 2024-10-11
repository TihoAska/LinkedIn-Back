using LinkedIn.Models.Posts;

namespace LinkedIn.Repository.IRepository
{
    public interface IReactionsRepository : IGenericRepository<Reaction>
    {
        Task<IEnumerable<Reaction>> GetReactionsForUserPost(int userPostId, CancellationToken cancellationToken);
    }
}
