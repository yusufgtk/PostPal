using SocialMedia.entity;

namespace SocialMedia.business.Abstract
{
    public interface IStoryServices
    {
        void Create(Story entity);
        void Delete(Story entity);
        void Update(Story entity);
        List<Story> GetStoriesByFollowUpUserId(string userId);
        List<Story> GetStories24HoursByFollowUpUserId(string userId);
        bool IsThereStory(string userId);
    }
}