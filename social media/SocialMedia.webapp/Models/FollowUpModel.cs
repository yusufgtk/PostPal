namespace SocialMedia.webapp.Models
{
    public class FollowUpModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } //Takip eden Kullanıcı id
        public string FollowUpUserId { get; set; } // Takip edilen kullanıcı kimliği
    }
}