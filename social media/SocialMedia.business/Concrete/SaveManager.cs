using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.business.Concrete
{
    public class SaveManager : ISaveServices
    {
        private readonly ISaveRepository _saveRepository;
        public SaveManager(ISaveRepository saveRepository)
        {
            _saveRepository=saveRepository;
        }
        public void Create(Save entity)
        {
            _saveRepository.Create(entity);
        }

        public void Delete(Save entity)
        {
            _saveRepository.Delete(entity);
        }

        public void DeleteMultiple(List<Save> entities)
        {
            _saveRepository.DeleteMultiple(entities);
        }

        public Save GetSaveByPostIdAndUserId(int postId, string userId)
        {
            return _saveRepository.GetSaveByPostIdAndUserId(postId,userId);
        }

        public List<Save> GetSavesByPostId(int postId)
        {
            return _saveRepository.GetSavesByPostId(postId);
        }

        public List<Save> GetSavesByUserId(string userId)
        {
            return _saveRepository.GetSavesByUserId(userId);
        }

        public bool IsSavePost(int postId, string userId)
        {
            return _saveRepository.IsSavePost(postId,userId);
        }

        public int TotalSavesCount(string userId)
        {
            return _saveRepository.TotalSavesCount(userId);
        }

        public void Update(Save entity)
        {
            _saveRepository.Update(entity);
        }
    }
}