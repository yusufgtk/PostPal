using Microsoft.AspNetCore.Identity;

namespace SocialMedia.webapp.Identity
{
    public class User:IdentityUser
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }

        public DateTime SendMessageTime { get; set; }

    }
}