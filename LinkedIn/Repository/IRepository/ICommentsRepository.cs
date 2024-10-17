using LinkedIn.Models.Posts;

namespace LinkedIn.Repository.IRepository
{
    public interface ICommentsRepository : IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAllWithUser(CancellationToken cancellationToken);
        Task<Comment> GetById(int id, CancellationToken cancellationToken);
    }
}
