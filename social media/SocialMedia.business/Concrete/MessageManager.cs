using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class MessageManager : IMessageServices
    {
        private readonly IMessageRepository _messageRepository;
        public MessageManager(IMessageRepository messageRepository)
        {
            _messageRepository=messageRepository;
        }
        public void Create(Message entity)
        {
            _messageRepository.Create(entity);
        }

        public void Delete(Message entity)
        {
            _messageRepository.Delete(entity);
        }

        public List<Message> GetMessagesBySenderIdAndReceiverId(string SenderId, string ReceiverId)
        {
            return _messageRepository.GetMessagesBySenderIdAndReceiverId(SenderId,ReceiverId);
        }

        public List<Message> GetMessagesByUserId(string userId)
        {
            return _messageRepository.GetMessagesByUserId(userId);
        }

        public int TotalMessageCount(string SenderId, string ReceiverId)
        {
            return _messageRepository.TotalMessageCount(SenderId,ReceiverId);
        }

        public void Update(Message entity)
        {
            _messageRepository.Update(entity);
        }
    }
}