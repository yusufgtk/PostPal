namespace SocialMedia.entity
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; } // bildirimin gönderildiği kişi
        public string UserId2 { get; set; } // Bildirimi gönderen kişi
        public int PostId { get; set; }
        public string NotificationType { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Seen { get; set; }

    }
}