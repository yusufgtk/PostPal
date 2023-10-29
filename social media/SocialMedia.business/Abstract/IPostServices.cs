using SocialMedia.entity;

namespace SocialMedia.business.Abstract
{
    public interface IPostServices
    {
        void Create(Post entity);
        void Delete(Post entity);
        void DeleteMultiple(List<Post> entities);
        void Update(Post entity);
        List<Post> GetAll();
        List<Post> GetHomePosts(string userId);
        List<Post> GetDiscoverPosts();
        Post GetPostById(int id);
        int TotalPostsCount(string userId);
        List<Post> GetPostsByUserId(string userId);
    }
}