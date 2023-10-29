using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.business.Concrete
{
    public class StoryManager : IStoryServices
    {
        private readonly IStoryRepository _storyRepository;
        public StoryManager(IStoryRepository storyRepository)
        {
            _storyRepository=storyRepository;
        }
        public void Create(Story entity)
        {
            _storyRepository.Create(entity);
        }

        public void Delete(Story entity)
        {
            _storyRepository.Delete(entity);
        }

        public List<Story> GetStories24HoursByFollowUpUserId(string userId)
        {
            return _storyRepository.GetStories24HoursByFollowUpUserId(userId);
        }

        public List<Story> GetStoriesByFollowUpUserId(string userId)
        {
            return _storyRepository.GetStoriesByFollowUpUserId(userId);
        }

        public bool IsThereStory(string userId)
        {
            return _storyRepository.IsThereStory(userId);
        }

        public void Update(Story entity)
        {
            _storyRepository.Update(entity);
        }
    }
}