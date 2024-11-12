using LinkedIn.Data;
using LinkedIn.Models.Pages;
using LinkedIn.Models.Users;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Repository
{
    public class PageRepository : GenericRepository<Page>, IPageRepository
    {
        public PageRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task<Page> GetById(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(page => page.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Page> GetByName(string name, CancellationToken cancellationToken)
        {
            return await _query.Where(page => page.Name == name)
                .Include(page => page.Followers)
                .FirstOrDefaultAsync(cancellationToken);  
        }

        public async Task<IEnumerable<Page>> GetAllForUser(int userId, CancellationToken cancellationToken)
        {
            return await _query.Where(page => page.Followers.Any(follower => follower.Id == userId)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Page>> GetPagesYouMightLike(int id, CancellationToken cancellationToken)
        {
            return await _query
                .Include(page => page.Followers)
                .Where(page => !page.Followers.Any(follower => follower.Id == id))
                .Take(2)
                .ToListAsync(cancellationToken);
        }
    }
}
