using LinkedIn.Data;
using LinkedIn.Repository.IRepository;
using LinkedIn.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace LinkedIn.Repository
{
    public class ConnectionsRepository : GenericRepository<PendingConnections>, IConnectionsRepository
    {
        public ConnectionsRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task<IEnumerable<PendingConnections>> GetAllPendingConnectionsWithSenderAndReceiver(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(pendingConnection => (pendingConnection.SenderId == id || pendingConnection.ReceiverId == id) && pendingConnection.Status == "Pending")
                .Include(pc => pc.Sender)
                .Include(pc => pc.Receiver)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<PendingConnections>> GetAllAcceptedConnectionsWithSenderAndReceiver(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(pendingConnection => (pendingConnection.SenderId == id || pendingConnection.ReceiverId == id) && pendingConnection.Status == "Accepted")
                .Include(pc => pc.Receiver)
                .Include(pc => pc.Sender)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<PendingConnections>> GetAllConnectionsWithSenderAndReceiver(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(pendingConnection => pendingConnection.SenderId == id || pendingConnection.ReceiverId == id)
                .Include(pc => pc.Receiver)
                .Include(pc => pc.Sender)
                .ToListAsync(cancellationToken);
        }
    }
}
