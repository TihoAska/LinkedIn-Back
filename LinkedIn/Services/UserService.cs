using AutoMapper;
using LinkedIn.Models.Users;
using LinkedIn.Repository;
using LinkedIn.JWTFeatures;
using LinkedIn.Repository.IRepository;
using LinkedIn.Services.IServices;
using Microsoft.AspNetCore.Identity;
using LinkedIn.Models.Account;
using LinkedIn.Models.Pages;

namespace LinkedIn.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        IMapper _autoMapper;
        UserManager<User> _userManager;
        JWTHandler _jwtHandler;

        public UserService(
            IUnitOfWork unitOfWork,
            IMapper autoMapper,
            UserManager<User> userManager,
            JWTHandler jwtHandler)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<AuthResponse> Create(UserCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetByEmail(createRequest.Email, cancellationToken);

            if (existingUser != null)
            {
                throw new Exception("User with the given email already exists!");
            }

            var newUser = _autoMapper.Map<User>(createRequest);
            newUser.UserName = createRequest.FirstName + createRequest.LastName + Guid.NewGuid();

            var result = await _userManager.CreateAsync(newUser, createRequest.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                throw new Exception(string.Join(",", errors));
            }

            if (createRequest.ImageUrl == null)
            {
                newUser.ImageUrl = "../../../assets/images/avatars/avatar-male-1.png";
            }

            var token = _jwtHandler.GenerateJWTToken(newUser);

            if (string.IsNullOrEmpty(newUser.RefreshToken))
            {
                newUser.RefreshToken = _jwtHandler.GenerateJWTRefreshToken();
            }

            newUser.NormalizedEmail = createRequest.Email.ToUpper();
            newUser.DateCreated = DateTime.Now.ToUniversalTime();
            newUser.DateModified = DateTime.Now.ToUniversalTime();

            await _unitOfWork.SaveChangesAsync();

            return new AuthResponse { IsAuthSuccessful = true, AccessToken = token, RefreshToken = newUser.RefreshToken };
        }

        public async Task<AuthResponse> Login(UserLoginRequest createRequest, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByEmail(createRequest.Email, cancellationToken);

            if (userFromDb == null)
            {
                return new AuthResponse { IsAuthSuccessful = false, ErrorMessage = "Incorrect email", Type = "Email" };
            }

            if (!await _userManager.CheckPasswordAsync(userFromDb, createRequest.Password))
            {
                return new AuthResponse { IsAuthSuccessful = false, ErrorMessage = "Incorrect password!", Type = "Password" };
            }

            var token = _jwtHandler.GenerateJWTToken(userFromDb);

            if (string.IsNullOrEmpty(userFromDb.RefreshToken))
            {
                userFromDb.RefreshToken = _jwtHandler.GenerateJWTRefreshToken();
            }

            userFromDb.LastLogin = DateTime.Now.ToUniversalTime();

            await _unitOfWork.SaveChangesAsync();

            return new AuthResponse { IsAuthSuccessful = true, AccessToken = token, RefreshToken = userFromDb.RefreshToken };
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(id, cancellationToken);

            _unitOfWork.Users.Remove(userFromDb);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
        {
            var usersFromDb = await _unitOfWork.Users.GetAll(cancellationToken);

            return usersFromDb;
        }

        public async Task<User> GetAllUserConnections(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetAllConnections(id, cancellationToken);

            return userFromDb;
        }

        public async Task<User> GetAllUserFollowers(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetAllFollowers(id, cancellationToken);

            return userFromDb;
        }

        public async Task<User> GetAllUserFollowing(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetAllFollowing(id, cancellationToken);

            return userFromDb;
        }

        public async Task<User> GetAllUserFollowingPages(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetAllPages(id, cancellationToken);

            return userFromDb;
        }

        public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByEmail(email, cancellationToken);

            return userFromDb;
        }

        public async Task<User> GetById(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(id, cancellationToken);

            if(userFromDb == null)
            {
                throw new Exception("User with the given id was not found!");
            }

            var connections = await _unitOfWork.Connections.GetAllConnectionsWithSenderAndReceiver(id, cancellationToken);
            var pagesFollowing = await _unitOfWork.Pages.GetAllForUser(id, cancellationToken);

            userFromDb.Connections = new List<User>();
            userFromDb.PagesFollowing = new List<Page>();
            userFromDb?.Education?.OrderByDescending(x => x.StartTime);

            userFromDb.PagesFollowing.AddRange(pagesFollowing);

            foreach (var connection in connections)
            {
                if (connection.SenderId != id && connection.ReceiverId == id)
                {
                    userFromDb.Connections.Add(connection.Sender);
                }
                else if (connection.ReceiverId != id && connection.SenderId == id)
                {
                    userFromDb.Connections.Add(connection.Receiver);
                }
            }

            return userFromDb;
        }

        public async Task<User> GetByIdWithUserDetails(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(id, cancellationToken);

            if(userFromDb == null)
            {
                throw new Exception("User with the given ID was not found!");
            }

            var connections = await _unitOfWork.Connections.GetAllConnectionsWithSenderAndReceiver(id, cancellationToken);
            var pagesFollowing = await _unitOfWork.Pages.GetAllForUser(id, cancellationToken);

            userFromDb.Connections = new List<User>();
            userFromDb.PagesFollowing = new List<Page>();
            userFromDb.PagesFollowing.AddRange(pagesFollowing);
            userFromDb.Education = userFromDb?.Education?.OrderByDescending(x => x.StartTime).ToList();

            foreach (var connection in connections)
            {
                if (connection.SenderId != id && connection.ReceiverId == id)
                {
                    userFromDb.Connections.Add(connection.Sender);
                }
                else if (connection.ReceiverId != id && connection.SenderId == id)
                {
                    userFromDb.Connections.Add(connection.Receiver);
                }
            }

            return userFromDb;
        }

        public async Task<User> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByPhoneNumber(phoneNumber, cancellationToken);

            return userFromDb;
        }

        public async Task<User> GetByUserName(string userName, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByUserName(userName, cancellationToken);

            return userFromDb;
        }

        public async Task<IEnumerable<User>> GetFiveUsers(CancellationToken cancellationToken)
        {
            IEnumerable<User> usersFromDb;
            HashSet<int> userIds = new HashSet<int>();

            do
            {
                usersFromDb = await _unitOfWork.Users.GetFiveUsers(cancellationToken);
                userIds = usersFromDb.Select(u => u.Id).ToHashSet();
            }
            while (userIds.Count < 5);

            return usersFromDb;
        }

        public async Task<IEnumerable<User>> GetPeopleYouMayKnow(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(id, cancellationToken) ?? throw new Exception("User with the id" + id + " was not found!");
            var usersFromDb = await _unitOfWork.Users.GetAll(cancellationToken);
            var pendingConnections = await _unitOfWork.Connections.GetAllConnectionsWithSenderAndReceiver(id, cancellationToken);

            var peopleYouMayKnow = usersFromDb.Where(user => (user.Connections == null || !user.Connections.Contains(userFromDb)) && user.Id != id);

            return peopleYouMayKnow;
        }

        public async Task<IEnumerable<User>> GetOtherSimilarProfiles(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(id, cancellationToken);
            var usersFromDb = await _unitOfWork.Users.GetAll(cancellationToken);

            var otherSimilarProfiles = usersFromDb.Where(user => user.Connections != null && user.Connections.Contains(userFromDb) && user.Id != id);

            return otherSimilarProfiles;
        }

        public async Task<IEnumerable<User>> GetFivePeopleYouMayKnow(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(id, cancellationToken) ?? throw new Exception("User with the id" + id + " was not found!");
            var usersFromDb = await _unitOfWork.Users.GetAll(cancellationToken);
            var pendingConnections = await _unitOfWork.Connections.GetAllConnectionsWithSenderAndReceiver(id, cancellationToken); //dvojica kojima sam poslao connect

            var peopleYouMayKnow = usersFromDb.Where(user => (user.Connections == null || !user.Connections.Contains(userFromDb)) && user.Id != id).ToList(); //sve usere osim mene
            var filteredPeopleYouMayKnow = new List<User>();

            foreach(var user in peopleYouMayKnow)
            {
                if(!pendingConnections.Any(pc => pc.SenderId == id && pc.ReceiverId == user.Id))
                {
                    filteredPeopleYouMayKnow.Add(user);
                }
            }

            return filteredPeopleYouMayKnow.Take(5);
        }

        public async Task<IEnumerable<User>> GetFiveOtherSimilarProfiles(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(id, cancellationToken) ?? throw new Exception("User with the id" + id + " was not found!");
            var usersFromDb = await _unitOfWork.Users.GetAll(cancellationToken);

            var connections = await _unitOfWork.Connections.GetAll(cancellationToken);
            var userConnections = connections.Where(connection => (connection.SenderId == id || connection.ReceiverId == id) && connection.Status == "Accepted").Take(5).ToList();

            var otherSimilarProfiles = new List<User>();

            foreach (var connection in userConnections)
            {
                if(connection.SenderId != id && connection.ReceiverId == id)
                {
                    otherSimilarProfiles.Add(connection.Sender);
                }
                else if (connection.ReceiverId != id && connection.SenderId == id)
                {
                    otherSimilarProfiles.Add(connection.Receiver);
                }
            }

            return otherSimilarProfiles;
        }

        public async Task<PendingConnections> SendConnection(int senderId, int receiverId, CancellationToken cancellationToken)
        {
            var senderFromDb = await _unitOfWork.Users.GetById(senderId, cancellationToken) ?? throw new Exception("Sender was not found!");
            var receiverFromDb = await _unitOfWork.Users.GetById(receiverId, cancellationToken) ?? throw new Exception("Receiver was not found!");

            var pendingConnections = await _unitOfWork.Connections.GetAllConnectionsWithSenderAndReceiver(senderId, cancellationToken);

            if(pendingConnections.Any(pc => pc.SenderId == senderId && pc.ReceiverId == receiverId)){
                throw new Exception("Connection already exists");
            }

            var newPendingConnection = new PendingConnections()
            {
                Sender = senderFromDb,
                Receiver = receiverFromDb,
                ReceiverId = receiverId,
                SenderId = senderId,
                RequestDate = DateTime.Now.ToUniversalTime(),
                Status = "Pending"
            };

            _unitOfWork.Connections.Add(newPendingConnection);
            await _unitOfWork.SaveChangesAsync();

            return newPendingConnection;
        }

        public async Task<IEnumerable<PendingConnections>> GetAllSentConnections(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(id, cancellationToken) ?? throw new Exception("User was not found!");
            var pendingConnections = await _unitOfWork.Connections.GetAll(cancellationToken);

            var sentConnections = pendingConnections.Where(pc => pc.SenderId == id && pc.Status == "Pending");

            return sentConnections;
        }

        public async Task<IEnumerable<PendingConnections>> GetAllReceivedConnections(int id, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(id, cancellationToken);
            var pendingConnections = await _unitOfWork.Connections.GetAll(cancellationToken);

            var receivedConnections = pendingConnections.Where(pc => pc.ReceiverId == id && pc.Status == "Pending");

            return receivedConnections;
        }

        public async Task<IEnumerable<PendingConnections>> GetAllPendingConnections(int id, CancellationToken cancellationToken)
        {
            var pendingConnections = await _unitOfWork.Connections.GetAllPendingConnectionsWithSenderAndReceiver(id, cancellationToken);

            return pendingConnections;
        }

        public async Task<PendingConnections> AcceptConnection(int senderId, int receiverId, CancellationToken cancellationToken)
        {
            var senderFromDb = await _unitOfWork.Users.GetById(senderId, cancellationToken);
            var receiverFromDb = await _unitOfWork.Users.GetById(receiverId, cancellationToken);

            var pendingConnections = await _unitOfWork.Connections.GetAll(cancellationToken);
            var pendingConnection = pendingConnections.Where(pc => pc.SenderId == senderId && pc.ReceiverId == receiverId && pc.Status == "Pending").FirstOrDefault();

            if(pendingConnection == null)
            {
                throw new Exception("No connections between ID:" + senderId + "and ID:" + receiverId);
            }

            pendingConnection.Status = "Accepted";

            if(senderFromDb.Connections == null)
            {
                senderFromDb.Connections = new List<User>();
            }

            if (receiverFromDb.Connections == null)
            {
                receiverFromDb.Connections = new List<User>();
            }

            senderFromDb.Connections.Add(receiverFromDb);
            receiverFromDb.Connections.Add(senderFromDb);

            await _unitOfWork.SaveChangesAsync();

            return pendingConnection;
        }

        public async Task<bool> RejectConnection(int senderId, int receiverId, CancellationToken cancellationToken)
        {
            var senderFromDb = await _unitOfWork.Users.GetById(senderId, cancellationToken);
            var receiverFromDb = await _unitOfWork.Users.GetById(receiverId, cancellationToken);

            var pendingConnections = await _unitOfWork.Connections.GetAll(cancellationToken);
            var pendingConnection = pendingConnections.Where(pc => pc.SenderId == senderId && pc.ReceiverId == receiverId && pc.Status == "Pending").FirstOrDefault();
            
            if (pendingConnection == null)
            {
                throw new Exception("No connections between ID:" + senderId + "and ID:" + receiverId);
            }

            _unitOfWork.Connections.Remove(pendingConnection);
            await _unitOfWork.SaveChangesAsync();

            return true;

        public async Task<PendingConnections> WithdrawSentConnection(int senderId, int receiverId, CancellationToken cancellationToken)
        {
            var senderFromDb = await _unitOfWork.Users.GetById(senderId, cancellationToken);
            var receiverFromDb = await _unitOfWork.Users.GetById(receiverId, cancellationToken);

            var pendingConnections = await _unitOfWork.Connections.GetAll(cancellationToken);
            var pendingConnection = pendingConnections.Where(pc => pc.SenderId == senderId && pc.ReceiverId == receiverId && pc.Status == "Pending").FirstOrDefault();

            if (pendingConnection != null)
            {
                _unitOfWork.Connections.Remove(pendingConnection);
            }
            else
            {
                throw new Exception("No connection found!");
            }

            await _unitOfWork.SaveChangesAsync();
            return pendingConnection;
        }

        public async Task<User> Update(UserUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(updateRequest.Id, cancellationToken);

            userFromDb.FirstName = updateRequest.FirstName;
            userFromDb.LastName = updateRequest.LastName;
            userFromDb.Email = updateRequest.Email;
            userFromDb.PhoneNumber = updateRequest.PhoneNumber;
            userFromDb.ImageUrl = updateRequest.ImageUrl;

            _unitOfWork.Users.Update(userFromDb);
            await _unitOfWork.SaveChangesAsync();

            return userFromDb;
        }
    }
}
