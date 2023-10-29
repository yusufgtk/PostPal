using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.business.Concrete
{
    public class CommentManager : ICommentServices
    {
        private readonly ICommentRepository _commentRepository;
        public CommentManager(ICommentRepository commentRepository)
        {
            _commentRepository=commentRepository;
        }
        public void Create(Comment entity)
        {
            _commentRepository.Create(entity);
        }

        public void Delete(Comment entity)
        {
            _commentRepository.Delete(entity);
        }

        public void DeleteMultiple(List<Comment> entities)
        {
            _commentRepository.DeleteMultiple(entities);
        }

        public Comment GetCommentById(int id)
        {
            return _commentRepository.GetCommentById(id);
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            return _commentRepository.GetCommentsByPostId(postId);
        }

        public int TotalCommentsCountByPostId(int postId)
        {
            return _commentRepository.TotalCommentsCountByPostId(postId);
        }

        public void Update(Comment entity)
        {
            _commentRepository.Update(entity);
        }
    }
}