using System.ComponentModel.DataAnnotations;

namespace SocialMedia.webapp.Models
{
    public class StoryModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public string ImageUrl { get; set; }
        
        [MaxLength(100,ErrorMessage ="100 karakteri aşamazsınız!")]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}