using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface IFollowUpRepository : IRepository<FollowUp>
    {
        List<FollowUp> GetFollowUps(string userId);
        FollowUp GetFollowUpByUserId(string userId,string followUpUserId);
        int TotalFollowUpCount(string userId);
    }
}