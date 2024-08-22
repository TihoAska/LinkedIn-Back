using LinkedIn.Models.Users;

namespace LinkedIn.Repository.IRepository
{
    public interface IConnectionsRepository : IGenericRepository<PendingConnections>
    {
        Task<IEnumerable<PendingConnections>> GetAllPendingConnectionsWithSenderAndReceiver(int id, CancellationToken cancellationToken);
        Task<IEnumerable<PendingConnections>> GetAllAcceptedConnectionsWithSenderAndReceiver(int id, CancellationToken cancellationToken);
        Task<IEnumerable<PendingConnections>> GetAllConnectionsWithSenderAndReceiver(int id, CancellationToken cancellationToken);
    }
}
