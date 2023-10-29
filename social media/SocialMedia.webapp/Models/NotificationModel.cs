namespace SocialMedia.webapp.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } // bildirimin gönderildiği kişi
        public string UserId2 { get; set; } // Bildirimi gönderen kişi
        public string UserName { get; set; } // Bildirimi gönderen kişi kullanıcı adı
        public int PostId { get; set; }
        public string NotificationType { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Seen { get; set; }
    }
    
}