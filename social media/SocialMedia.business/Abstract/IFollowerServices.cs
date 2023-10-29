using SocialMedia.entity;

namespace SocialMedia.business.Abstract
{
    public interface IFollowerServices
    {
        void Create(Follower entity);
        void Delete(Follower entity);
        void Update(Follower entity);
        List<Follower> GetAll();
        List<Follower> GetFollowers(string userId);
        Follower GetFollowerByUserId(string userId,string followerUserId);
        int TotalFollowerCount(string userId);
    }
}