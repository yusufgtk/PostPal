using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.business.Concrete
{
    public class NotificationManager : INotificationServices
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationManager(INotificationRepository notificationRepository)
        {
            _notificationRepository=notificationRepository;
        }
        public void Create(Notification entity)
        {
            _notificationRepository.Create(entity);
        }

        public void Delete(Notification entity)
        {
            _notificationRepository.Delete(entity);
        }

        public void DeleteMultiple(List<Notification> entities)
        {
            _notificationRepository.DeleteMultiple(entities);
        }

        public void DeleteNotification(int postId, string userId2)
        {
            _notificationRepository.DeleteNotification(postId,userId2);
        }

        public void DeleteNotification(string userId, string userId2)
        {
            _notificationRepository.DeleteNotification(userId,userId2);
        }

        public Notification GetNotificationById(int Id)
        {
            return _notificationRepository.GetNotificationById(Id);
        }

        public List<Notification> GetNotificationByUserId(string userId)
        {
            return _notificationRepository.GetNotificationByUserId(userId);
        }

        public List<Notification> GetNotificationsByPost(int postId)
        {
            return _notificationRepository.GetNotificationsByPost(postId);
        }

        public void Update(Notification entity)
        {
            _notificationRepository.Update(entity);
        }
    }
}