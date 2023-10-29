using SocialMedia.entity;

namespace SocialMedia.business.Abstract
{
    public interface ISaveServices
    {
        void Create(Save entity);
        void Delete(Save entity);
        void Update(Save entity);
        void DeleteMultiple(List<Save> entities);
        int TotalSavesCount(string userId);
        List<Save> GetSavesByUserId(string userId);
        List<Save> GetSavesByPostId(int postId);
        bool IsSavePost(int postId, string userId);
        Save GetSaveByPostIdAndUserId(int postId, string userId);
    }
}