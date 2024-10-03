using LinkedIn.Data;
using LinkedIn.Models.Messages;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Repository
{
    public class MessagesRepository : GenericRepository<Message>, IMessagesRepository
    {
        public MessagesRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task<Message> GetById(int id, CancellationToken cancellationToken)
        {
            return await _query.Where(message => message.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Message>> GetAllByUserId(int userId, CancellationToken cancellationToken)
        {
            return await _query.Where(message => message.SenderId == userId || message.ReceiverId == userId).ToListAsync(cancellationToken);
        }
    }
}
