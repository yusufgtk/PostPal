using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface IFollowerRepository : IRepository<Follower>
    {
        List<Follower> GetFollowers(string userId);
        Follower GetFollowerByUserId(string userId,string followerUserId);
        int TotalFollowersCount(string userId);
    }
}