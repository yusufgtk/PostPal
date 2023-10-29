using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface IPostRepository : IRepository<Post>
    {
        List<Post> GetHomePosts(string userId);
        List<Post> GetDiscoverPosts();
        Post GetPostById(int id);
        int TotalPostsCount(string userId);
        List<Post> GetPostsByUserId(string userId);
    }
}