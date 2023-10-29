using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.business.Concrete
{
    public class FollowUpManager : IFollowUpServices
    {
        private readonly IFollowUpRepository _followUpRepository;
        public FollowUpManager(IFollowUpRepository followUpRepository)
        {
            _followUpRepository=followUpRepository;
        }
        public void Create(FollowUp entity)
        {
            _followUpRepository.Create(entity);
        }

        public void Delete(FollowUp entity)
        {
            _followUpRepository.Delete(entity);
        }

        public List<FollowUp> GetAll()
        {
            return _followUpRepository.GetAll();
        }

        public FollowUp GetFollowUpByUserId(string userId,string followUpUserId)
        {
            return _followUpRepository.GetFollowUpByUserId(userId,followUpUserId);
        }

        public List<FollowUp> GetFollowUps(string userId)
        {
            return _followUpRepository.GetFollowUps(userId);
        }

        public int TotalFollowUpCount(string userId)
        {
            return _followUpRepository.TotalFollowUpCount(userId);
        }

        public void Update(FollowUp entity)
        {
            _followUpRepository.Update(entity);
        }
    }
}