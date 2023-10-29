using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Comment GetCommentById(int id);
        int TotalCommentsCountByPostId(int postId);
        List<Comment> GetCommentsByPostId(int postId);
    }
}