namespace SocialMedia.entity
{
    public class Post
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string? Content { get; set; }
        public string? PostType { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCommentOpen { get; set; }

        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}