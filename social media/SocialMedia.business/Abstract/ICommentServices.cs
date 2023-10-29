using SocialMedia.entity;

namespace SocialMedia.business.Abstract
{
    public interface ICommentServices
    {
        void Create(Comment entity);
        void Delete(Comment entity);
        void DeleteMultiple(List<Comment> entities);
        void Update(Comment entity);
        Comment GetCommentById(int id);
        int TotalCommentsCountByPostId(int postId);
        List<Comment> GetCommentsByPostId(int postId);
    }
}