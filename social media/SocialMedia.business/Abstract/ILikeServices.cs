using SocialMedia.entity;

namespace SocialMedia.business.Abstract
{
    public interface ILikeServices
    {
        void Create(Like entity);
        void Delete(Like entity);
        void DeleteMultiple(List<Like> entities);
        void Update(Like entity);
        Like GetLikeById(int id);
        int TotalLikesCountByPostId(int postId);
        List<Like> GetLikesByPostId(int postId);
        bool IsLikePost(int postId, string userId);
        Like GetLikeByPostIdAndUserId(int postId, string userId);
    }
}