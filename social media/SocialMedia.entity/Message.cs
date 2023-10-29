namespace  SocialMedia.entity
{
    public class Message
    {
        public long Id { get; set; }
        public string SenderUserId { get; set; } //gönderici
        public string ReceiverUserId { get; set; } //alıcı
        public string Content { get; set; } 
        public DateTime CreatedAt  { get; set; } 
        public string? BackgroundColor { get; set; } 

        
    }
}