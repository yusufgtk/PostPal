using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class FollowerRepository : Repository<Follower, DataContext>, IFollowerRepository
    {
        public Follower GetFollowerByUserId(string userId,string followerUserId)
        {
            using(var context = new DataContext())
            {
                return context.Followers.Where(i=>i.UserId==userId&&i.FollowerUserId==followerUserId).FirstOrDefault();
            }
        }

        public List<Follower> GetFollowers(string userId)
        {
            using(var context = new DataContext())
            {
                return context.Followers.Where(i=>i.UserId == userId).ToList();
            }
        }

        public int TotalFollowersCount(string userId)
        {
            using(var context = new DataContext())
            {
                return context.Followers.Where(i=>i.UserId == userId).Count();
            }
        }
    }
}