using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.business.Concrete
{
    public class FollowerManager : IFollowerServices
    {
        private readonly IFollowerRepository _followerRepository;
        public FollowerManager(IFollowerRepository followerRepository)
        {
            _followerRepository=followerRepository;
        }
        public void Create(Follower entity)
        {
            _followerRepository.Create(entity);
        }

        public void Delete(Follower entity)
        {
            _followerRepository.Delete(entity);
        }

        public List<Follower> GetAll()
        {
            return _followerRepository.GetAll();
        }

        public Follower GetFollowerByUserId(string userId,string followerUserId)
        {
            return _followerRepository.GetFollowerByUserId(userId,followerUserId);
        }

        public List<Follower> GetFollowers(string userId)
        {
            return _followerRepository.GetFollowers(userId);
        }

        public int TotalFollowerCount(string userId)
        {
            return _followerRepository.TotalFollowersCount(userId);
        }

        public void Update(Follower entity)
        {
            _followerRepository.Update(entity);
        }
    }
}