using SocialMedia.entity;

namespace SocialMedia.business.Abstract
{
    public interface INotificationServices
    {
        void Create(Notification entity);
        void Delete(Notification entity);
        void Update(Notification entity);
        void DeleteMultiple(List<Notification> entities);
        List<Notification> GetNotificationByUserId(string userId);
        List<Notification> GetNotificationsByPost(int postId);
        Notification GetNotificationById(int Id);
        void DeleteNotification(int postId, string userId2);
        void DeleteNotification(string userId, string userId2);
    }
}