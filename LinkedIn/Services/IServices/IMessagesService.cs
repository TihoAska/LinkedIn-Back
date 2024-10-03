using LinkedIn.Models.Messages;

namespace LinkedIn.Services.IServices
{
    public interface IMessagesService
    {
        Task<Message> SendMessage(MessageCreateRequest createRequest, CancellationToken cancellationToken);
        Task<Message> DeleteMessage(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Message>> GetAllMessagesForUser(int userId, CancellationToken cancellationToken);
    }
}
