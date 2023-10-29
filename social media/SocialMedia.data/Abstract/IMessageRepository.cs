using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface IMessageRepository : IRepository<Message>
    {
        int TotalMessageCount(string SenderId, string ReceiverId);
        List<Message> GetMessagesByUserId(string userId);
        List<Message> GetMessagesBySenderIdAndReceiverId(string SenderId, string ReceiverId);
    }
}