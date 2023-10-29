namespace SocialMedia.webapp.Models
{
    public class BlueTickApplicationModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Description { get; set; }
        public bool IsApplication { get; set; }
    }
}