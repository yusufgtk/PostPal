using System.ComponentModel.DataAnnotations;

namespace SocialMedia.webapp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Zorunlu alan!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}