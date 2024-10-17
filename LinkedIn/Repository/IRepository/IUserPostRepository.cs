using LinkedIn.Models.Posts;

namespace LinkedIn.Repository.IRepository
{
    public interface IUserPostRepository : IGenericRepository<UserPost>
    {
        Task<UserPost> GetById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<UserPost>> GetAllForUser(int userId, CancellationToken cancellationToken);
        Task<IEnumerable<UserPost>> GetAllWithUser(CancellationToken cancellationToken);
    }
}
