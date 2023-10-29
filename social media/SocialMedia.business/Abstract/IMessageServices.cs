using SocialMedia.business;
using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface IMessageServices
    {
        void Create(Message entity);
        void Delete(Message entity);
        void Update(Message entity);
        int TotalMessageCount(string SenderId, string ReceiverId);
        List<Message> GetMessagesByUserId(string userId);
        List<Message> GetMessagesBySenderIdAndReceiverId(string SenderId, string ReceiverId);
    }
}