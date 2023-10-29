using System.ComponentModel.DataAnnotations;

namespace SocialMedia.webapp.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage ="Zorunlu alan!")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Zorunlu alan!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Zorunlu alan!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler aynı değil!")]
        public string ConfirmPassword { get; set; }

    }
}