using System.ComponentModel.DataAnnotations;

namespace SocialMedia.webapp.Models
{
    public class AddPostModel
    {

        [MaxLength(100,ErrorMessage ="Max 100 karakter olabilir!")]
        public string Content { get; set; }
        public bool IsCommentOpen { get; set; }
        
    }
}