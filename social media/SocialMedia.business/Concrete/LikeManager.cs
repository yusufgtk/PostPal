using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace  SocialMedia.business.Concrete
{
    public class LikeManager : ILikeServices
    {
        private readonly ILikeRepository _likeRepository;
        public LikeManager(ILikeRepository likeRepository)
        {
            _likeRepository=likeRepository;
        }
        public void Create(Like entity)
        {
            _likeRepository.Create(entity);
        }

        public void Delete(Like entity)
        {
            _likeRepository.Delete(entity);
        }

        public void DeleteMultiple(List<Like> entities)
        {
            _likeRepository.DeleteMultiple(entities);
        }

        public Like GetLikeById(int id)
        {
            return _likeRepository.GetLikeById(id);
        }

        public Like GetLikeByPostIdAndUserId(int postId, string userId)
        {
            return _likeRepository.GetLikeByPostIdAndUserId(postId,userId);
        }

        public List<Like> GetLikesByPostId(int postId)
        {
            return _likeRepository.GetLikesByPostId(postId);
        }

        public bool IsLikePost(int postId, string userId)
        {
            return _likeRepository.IsLikePost(postId,userId);
        }

        public int TotalLikesCountByPostId(int postId)
        {
            return _likeRepository.TotalLikesCountByPostId(postId);
        }

        public void Update(Like entity)
        {
            _likeRepository.Update(entity);
        }
    }
}