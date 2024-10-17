using LinkedIn.Models.Posts;
using LinkedIn.Models.Users;
using Microsoft.AspNetCore.Routing.Constraints;

namespace LinkedIn.Repository.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithEducations(CancellationToken cancellationToken);
        Task<User> GetById(int id, CancellationToken cancellationToken);
        Task<User> GetByIdWithUserDetails(int id, CancellationToken cancellationToken);
        Task<User> GetByUserName(string userName, CancellationToken cancellationToken);
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);
        Task<User> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken);
        Task<User> GetAllConnections(int id, CancellationToken cancellationToken);
        Task<User> GetAllFollowers(int id, CancellationToken cancellationToken);
        Task<User> GetAllFollowing(int id, CancellationToken cancellationToken);
        Task<User> GetAllPages(int id, CancellationToken cancellationToken);
        Task<User> GetByIdWithAllConnectionsAndPages(int id, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetFiveUsers(CancellationToken cancellationToken);
    }
}
