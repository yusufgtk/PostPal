namespace SocialMedia.webapp.Models
{
    public class ComplaintModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public string ImageUrl { get; set; }
        public string? Content { get; set; }
        public int NumberOfUser { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}