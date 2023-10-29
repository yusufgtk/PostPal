namespace SocialMedia.webapp.Models
{
    public class FollowerModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } //Takip edilen Kullanıcı id
        public string FollowerUserId { get; set; } // Takip eden kullanıcı kimliği
    }
}