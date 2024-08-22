using LinkedIn.Models.Account;
using LinkedIn.Models.Users;

namespace LinkedIn.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken);
        Task<User> GetById(int id, CancellationToken cancellationToken);
        Task<User> GetByIdWithUserDetails(int id, CancellationToken cancellationToken);
        Task<User> GetByUserName(string userName, CancellationToken cancellationToken);
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);
        Task<User> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken);
        Task<User> GetAllUserConnections(int id, CancellationToken cancellationToken);
        Task<User> GetAllUserFollowers(int id, CancellationToken cancellationToken);
        Task<User> GetAllUserFollowing(int id, CancellationToken cancellationToken);
        Task<User> GetAllUserFollowingPages(int id, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetPeopleYouMayKnow(int id, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetOtherSimilarProfiles(int id, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetFivePeopleYouMayKnow(int id, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetFiveOtherSimilarProfiles(int id, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetFiveUsers(CancellationToken cancellationToken);
        Task<IEnumerable<PendingConnections>> GetAllPendingConnections(int id, CancellationToken cancellationToken);
        Task<AuthResponse> Create(UserCreateRequest createRequest, CancellationToken cancellationToken);
        Task<AuthResponse> Login(UserLoginRequest loginRequest, CancellationToken cancellationToken);
        Task<User> Update(UserUpdateRequest updateRequest, CancellationToken cancellationToken);
        Task<PendingConnections> SendConnection(int senderId, int receiverId, CancellationToken cancellationToken);
        Task<PendingConnections> AcceptConnection(int senderId, int receiverId, CancellationToken cancellationToken);
        Task<bool> RejectConnection(int senderId, int receiverId, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
