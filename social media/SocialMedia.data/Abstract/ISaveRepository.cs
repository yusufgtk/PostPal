using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface ISaveRepository : IRepository<Save>
    {
        int TotalSavesCount(string userId);
        List<Save> GetSavesByUserId(string userId);
        List<Save> GetSavesByPostId(int postId);
        bool IsSavePost(int postId, string userId);
        Save GetSaveByPostIdAndUserId(int postId, string userId);
    }
}