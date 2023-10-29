namespace SocialMedia.webapp.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsLike { get; set; }
        public bool? IsSave { get; set; }
        public bool? IsCommentOpen { get; set; }
        public string? Content { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<LikeModel>? Likes { get; set; }
        public List<CommentModel>? Comments { get; set; }
    }

    public class LikeModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }

    }
    
    public class CommentModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }

    }
}