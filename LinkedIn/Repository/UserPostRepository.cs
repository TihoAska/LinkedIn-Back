using LinkedIn.Data;
using LinkedIn.Models.Posts;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace LinkedIn.Repository
{
    public class UserPostRepository : GenericRepository<UserPost>, IUserPostRepository
    {
        public UserPostRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task<UserPost> GetById(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(userPost => userPost.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserPost>> GetAllForUser(int userId, CancellationToken cancellationToken)
        {
            return await _query.Where(userPost => userPost.PosterId == userId)
                .Include(userPost => userPost.Reactions)
                    .ThenInclude(reaction => reaction.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserPost>> GetAllWithUser(CancellationToken cancellationToken)
        {
            return await _query
                .Include(userPost => userPost.User)
                .ToListAsync(cancellationToken);
        }
    }
}
