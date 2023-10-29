using SocialMedia.entity;

namespace SocialMedia.business.Abstract
{
    public interface IFollowUpServices
    {
        void Create(FollowUp entity);
        void Delete(FollowUp entity);
        void Update(FollowUp entity);
        List<FollowUp> GetAll();
        List<FollowUp> GetFollowUps(string userId);
        FollowUp GetFollowUpByUserId(string userId,string followUpUserId);
        int TotalFollowUpCount(string userId);
    }
}