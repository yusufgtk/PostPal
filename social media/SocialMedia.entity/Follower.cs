using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.entity
{
    public class Follower
    {
        public int Id { get; set; }
        public string UserId { get; set; } //Takip edilen Kullanıcı id
        public string FollowerUserId { get; set; } // Takip eden kullanıcı kimliği
    }
}