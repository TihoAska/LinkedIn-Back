using LinkedIn.Data;
using LinkedIn.Models.Users;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata.Ecma335;

namespace LinkedIn.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IDataContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<User>> GetAllWithEducations(CancellationToken cancellationToken)
        {
            return await _query
                .Include(user => user.Education)
                .Include(user => user.ProfileDetails)
                .ToListAsync(cancellationToken);
        }
        public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return await _query.Where(user => user.Email == email).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetById(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(user => user.Id == id)
                .Include(user => user.Education)
                .Include(user => user.ProfileDetails)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetByIdWithUserDetails(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(user => user.Id == id)
                .Include(user => user.ProfileDetails)
                .Include(user => user.Education)
                .Include(user => user.Experience)
                    .ThenInclude(experience => experience.Company)
                 .Include(user => user.Experience)
                    .ThenInclude(experience => experience.Location)
                .Include(user => user.Languages)
                .Include(user => user.Skills)
                .Include(user => user.LicensesAndCertifications)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetByIdWithAllConnectionsAndPages(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(user => user.Id == id)
                .Include(user => user.PagesFollowing)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetByUserName(string userName, CancellationToken cancellationToken)
        {
            return await _query.Where(user => user.UserName == userName).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken)
        {
            return await _query.Where(user => user.PhoneNumber == phoneNumber).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetAllConnections(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(user => user.Id == id)
                .Include(user => user.Connections)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetAllFollowers(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(user => user.Id == id)
                .Include(user => user.Followers)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetAllFollowing(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(user => user.Id == id)
                .Include(user => user.Following)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetAllPages(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(user => user.Id == id)
                .Include(user => user.PagesFollowing)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetFiveUsers(CancellationToken cancellationToken)
        {
            return await _query.OrderBy(x => Guid.NewGuid()).Take(5).ToListAsync(cancellationToken);
        }
    }
}
