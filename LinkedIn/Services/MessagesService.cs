using LinkedIn.Models.Messages;
using LinkedIn.Repository.IRepository;
using LinkedIn.Services.IServices;

namespace LinkedIn.Services
{
    public class MessagesService : IMessagesService
    {
        private IUnitOfWork _unitOfWork;

        public MessagesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;    
        }

        public async Task<IEnumerable<Message>> GetAllMessagesForUser(int userId, CancellationToken cancellationToken)
        {
            var messagesFromDb = await _unitOfWork.Messages.GetAllByUserId(userId, cancellationToken);

            if(messagesFromDb == null) 
            {
                return [];
            }

            return messagesFromDb;
        }


        public async Task<Message> SendMessage(MessageCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var senderFromDb = await _unitOfWork.Users.GetById(createRequest.SenderId, cancellationToken);

            if(senderFromDb == null)
            {
                throw new Exception("Sender with the given id " + createRequest.SenderId + " was not found!");
            }

            var receiverFromDb = await _unitOfWork.Users.GetById(createRequest.ReceiverId, cancellationToken);

            if (receiverFromDb == null)
            {
                throw new Exception("Receiver with the given id " + createRequest.ReceiverId + " was not found!");
            }

            var newMessage = new Message
            {
                SenderId = createRequest.SenderId,
                ReceiverId = createRequest.ReceiverId,
                Content = createRequest.Content,
                TimeSent = DateTime.UtcNow,
            };

            _unitOfWork.Messages.Add(newMessage);
            await _unitOfWork.SaveChangesAsync();

            return newMessage;
        }

        public async Task<Message> DeleteMessage(int id, CancellationToken cancellationToken)
        {
            var messageFromDb = await _unitOfWork.Messages.GetById(id, cancellationToken);

            if(messageFromDb == null) 
            {
                throw new Exception("Message with the given id " + id + " was not found!");
            }

            _unitOfWork.Messages.Remove(messageFromDb);
            await _unitOfWork.SaveChangesAsync();

            return messageFromDb;
        }
    }
}
