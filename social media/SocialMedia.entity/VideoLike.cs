namespace SocialMedia.entity
{
    public class VideoLike
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int VideoId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}