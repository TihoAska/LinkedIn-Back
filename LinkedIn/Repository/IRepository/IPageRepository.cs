using LinkedIn.Models.Pages;

namespace LinkedIn.Repository.IRepository
{
    public interface IPageRepository : IGenericRepository<Page>
    {
        Task<Page> GetById(int id, CancellationToken cancellationToken);
        Task<Page> GetByName(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Page>> GetAllForUser(int userId, CancellationToken cancellationToken);
        Task<IEnumerable<Page>> GetPagesYouMightLike(int id, CancellationToken cancellationToken);
    }
}
