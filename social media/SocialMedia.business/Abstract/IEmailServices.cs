namespace SocialMedia.business.Abstract
{
    public interface IEmailServices
    {
        void SendEmail(string toEmailAddress, string newPassword);
    }
}