using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface IStoryRepository:IRepository<Story>
    {
        List<Story> GetStoriesByFollowUpUserId(string userId);
        List<Story> GetStories24HoursByFollowUpUserId(string userId);
        bool IsThereStory(string userId);
    }
}