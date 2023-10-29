namespace SocialMedia.entity
{
    public class Feedback
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}