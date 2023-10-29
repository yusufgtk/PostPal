using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.business.Concrete
{
    public class BlueTickApplicationManager : IBlueTickApplicationServices
    {
        private readonly IBlueTickApplicationRepository _blueTickApplicationRepository;
        public BlueTickApplicationManager(IBlueTickApplicationRepository blueTickApplicationRepository)
        {
            _blueTickApplicationRepository=blueTickApplicationRepository;
        }
        public void Create(BlueTickApplication entity)
        {
            _blueTickApplicationRepository.Create(entity);
        }

        public void Delete(BlueTickApplication entity)
        {
            _blueTickApplicationRepository.Delete(entity);
        }

        public void DeleteReferences()
        {
            _blueTickApplicationRepository.DeleteReferences();
        }

        public BlueTickApplication GetBlueTickApplicationById(int id)
        {
            return _blueTickApplicationRepository.GetBlueTickApplicationById(id);
        }

        public BlueTickApplication GetBlueTickApplicationByUserId(string userId)
        {
            return _blueTickApplicationRepository.GetBlueTickApplicationByUserId(userId);
        }

        public List<BlueTickApplication> GetBlueTickApplications()
        {
            return _blueTickApplicationRepository.GetBlueTickApplications();
        }

        public void Update(BlueTickApplication entity)
        {
            _blueTickApplicationRepository.Update(entity);
        }
    }
}