using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace  SocialMedia.data.Concrete
{
    public class NotificationRepository : Repository<Notification, DataContext>, INotificationRepository
    {
        public void DeleteNotification(int postId, string userId2)
        {
            using(var context = new DataContext())
            {
                var notification = context.Notifications.Where(i=>i.PostId==postId&&i.UserId2==userId2).FirstOrDefault();
                if(notification!=null)
                {
                    context.Notifications.Remove(notification);
                    context.SaveChanges();
                }
            }
        }

        public void DeleteNotification(string userId, string userId2)
        {
            using(var context = new DataContext())
            {
                var notification = context.Notifications.Where(i=>i.UserId==userId&&i.UserId2==userId2&&i.NotificationType=="Follow").FirstOrDefault();
                if(notification!=null)
                {
                    context.Notifications.Remove(notification);
                    context.SaveChanges();
                }
            }
        }

        public Notification GetNotificationById(int Id)
        {
            using(var context = new DataContext())
            {
                return context.Notifications.Where(i=>i.Id==Id).FirstOrDefault();
            }
        }

        public List<Notification> GetNotificationByUserId(string userId)
        {
            using(var context = new DataContext())
            {
                return context.Notifications.Where(i=>i.UserId==userId).OrderByDescending(i=>i.CreatedAt).Take(50).ToList();
            }
        }

        public List<Notification> GetNotificationsByPost(int postId)
        {
            using(var context = new DataContext())
            {
                return context.Notifications.Where(i=>i.PostId==postId).ToList();
            }
        }

        public void Seed(string userId)
        {
            using(var context = new DataContext())
            {
                context.Notifications.Where(i=>i.UserId==userId).OrderByDescending(i=>i.CreatedAt).Take(50).ToList();
            }
        }
    }
}