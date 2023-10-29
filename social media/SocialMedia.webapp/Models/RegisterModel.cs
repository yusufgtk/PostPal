using System.ComponentModel.DataAnnotations;

namespace SocialMedia.webapp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Zorunlu alan!")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email formatında olması gerekiyor!")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Zorunlu alan!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Zorunlu alan!")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
        
        public void Clear()
        {
            this.UserName="";
            this.Email="";
            this.Password="";
            this.RePassword="";
        }
    }
}