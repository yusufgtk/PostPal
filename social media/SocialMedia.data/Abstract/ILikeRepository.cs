using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface ILikeRepository : IRepository<Like>
    {
        Like GetLikeById(int id);
        Like GetLikeByPostIdAndUserId(int postId, string userId);
        int TotalLikesCountByPostId(int postId);
        List<Like> GetLikesByPostId(int postId);
        bool IsLikePost(int postId, string userId);
    }
}