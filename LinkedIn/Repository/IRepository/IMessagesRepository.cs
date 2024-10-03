using LinkedIn.Models.Messages;

namespace LinkedIn.Repository.IRepository
{
    public interface IMessagesRepository : IGenericRepository<Message>
    {
        Task<Message> GetById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Message>>  GetAllByUserId (int userId, CancellationToken cancellationToken);
    }
}
