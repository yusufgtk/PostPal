using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.entity
{
    public class FollowUp
    {
        public int Id { get; set; }
        public string UserId { get; set; } //Takip eden Kullanıcı id
        public string FollowUpUserId { get; set; } // Takip edilen kullanıcı kimliği
    }
}