using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class FollowUpRepository : Repository<FollowUp, DataContext>, IFollowUpRepository
    {
        public FollowUp GetFollowUpByUserId(string userId,string followUpUserId)
        {
            using(var context = new DataContext())
            {
                return context.FollowUps.Where(i=>i.UserId==userId&&i.FollowUpUserId==followUpUserId).FirstOrDefault();
            }
        }

        public List<FollowUp> GetFollowUps(string userId)
        {
            using(var context = new DataContext())
            {
                return context.FollowUps.Where(i=>i.UserId==userId).ToList();
            }
        }

        public int TotalFollowUpCount(string userId)
        {
            using(var context = new DataContext())
            {
                return context.FollowUps.Where(i=>i.UserId == userId).Count();
            }
        }
    }
}