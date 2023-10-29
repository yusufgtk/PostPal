using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class MessageRepository : Repository<Message, DataContext>, IMessageRepository
    {
        public List<Message> GetMessagesBySenderIdAndReceiverId(string SenderId, string ReceiverId)
        {
            using(var context = new DataContext())
            {
                return context.Messages.Where(i=>i.SenderUserId==SenderId&&i.ReceiverUserId==ReceiverId).ToList();
            }
        }

        public List<Message> GetMessagesByUserId(string userId)
        {
            using(var context = new DataContext())
            {
                return context.Messages.Where(i=>i.SenderUserId==userId).ToList();
            }
        }

        public int TotalMessageCount(string SenderId, string ReceiverId)
        {
            using(var context = new DataContext())
            {
                return context.Messages.Where(i=>i.SenderUserId==SenderId&&i.ReceiverUserId==ReceiverId).Count();
            }
        }
    }
}