namespace SocialMedia.entity
{
    public class Save
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int PostId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}