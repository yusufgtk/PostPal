using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface INotificationRepository:IRepository<Notification>
    {
        List<Notification> GetNotificationByUserId(string userId);
        List<Notification> GetNotificationsByPost(int postId);
        Notification GetNotificationById(int Id);
        void Seed(string userId);
        void DeleteNotification(int postId, string userId2);
        void DeleteNotification(string userId, string userId2);

    }
}