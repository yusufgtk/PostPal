using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.business.Concrete
{
    public class PostManager : IPostServices
    {
        private readonly IPostRepository _postRepository;
        public PostManager(IPostRepository postRepository)
        {
            _postRepository=postRepository;
        }
        public void Create(Post entity)
        {
            _postRepository.Create(entity);
        }

        public void Delete(Post entity)
        {
            _postRepository.Delete(entity);
        }

        public void DeleteMultiple(List<Post> entities)
        {
            _postRepository.DeleteMultiple(entities);
        }

        public List<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public List<Post> GetDiscoverPosts()
        {
            return _postRepository.GetDiscoverPosts();
        }

        public List<Post> GetHomePosts(string userId)
        {
            return _postRepository.GetHomePosts(userId);
        }

        public Post GetPostById(int id)
        {
            return _postRepository.GetPostById(id);
        }

        public List<Post> GetPostsByUserId(string userId)
        {
            return _postRepository.GetPostsByUserId(userId);
        }

        public int TotalPostsCount(string userId)
        {
            return _postRepository.TotalPostsCount(userId);
        }

        public void Update(Post entity)
        {
            _postRepository.Update(entity);
        }
    }
}