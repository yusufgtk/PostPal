using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class StoryRepository : Repository<Story, DataContext>, IStoryRepository
    {
        public List<Story> GetStories24HoursByFollowUpUserId(string userId)
        {
            using(var context = new DataContext())
            {
                return context.Stories
                                    .Where(i=>i.UserId==userId)
                                    .AsEnumerable()
                                    .Where(s => (DateTime.Now - s.CreatedAt).TotalHours <= 24)
                                    .OrderByDescending(i=>i.CreatedAt)
                                    .ToList();
            }
        }

        public List<Story> GetStoriesByFollowUpUserId(string userId)
        {
            using(var context = new DataContext())
            {
                return context.Stories.Where(i=>i.UserId==userId).ToList();
            }
        }

        public bool IsThereStory(string userId)
        {
            using(var context = new DataContext())
            {
                var list = context.Stories.Where(i=>i.UserId==userId)
                                    .AsEnumerable()
                                    .Where(s => (DateTime.Now - s.CreatedAt).TotalHours <= 24).ToList();
                if(list.Count>0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}