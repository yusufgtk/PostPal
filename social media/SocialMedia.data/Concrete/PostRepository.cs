using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class PostRepository : Repository<Post, DataContext>, IPostRepository
    {
        public List<Post> GetDiscoverPosts()
        {
            using(var context = new DataContext())
            {
                return context.Posts.OrderByDescending(i=>i.CreatedAt).Take(100).ToList();
            }
        }

        public List<Post> GetHomePosts(string userId)
        {
            using(var context = new DataContext())
            {
                var followUps = new List<FollowUp>(){};
                followUps = context.FollowUps.Where(i=>i.UserId==userId).ToList();
                var posts = new List<Post>(){};
                foreach(var followUp in followUps)
                {
                    var posts3 = context.Posts.Where(i=>i.UserId==followUp.
                    FollowUpUserId).OrderByDescending(i=>i.CreatedAt).Take(3);
                    foreach(var post in posts3)
                    {
                        if(post!=null)
                    {
                        posts.Add(post);
                    }
                    }
                    
                    
                }
                var iPosts= context.Posts.Where(i=>i.UserId==userId).OrderByDescending(i=>i.CreatedAt).Take(3);
                foreach(var iPost in iPosts)
                {
                    if(iPost!=null)
                    {
                        posts.Add(iPost);
                    }
                }
                
                return posts;
            }
        }

        public Post GetPostById(int id)
        {
            using(var context = new DataContext())
            {
                return context.Posts.FirstOrDefault(i=>i.Id==id);
            }
        }

        public List<Post> GetPostsByUserId(string userId)
        {
            using(var context = new DataContext())
            {
                return context.Posts.Where(i=>i.UserId==userId).OrderByDescending(i=>i.CreatedAt).ToList();
            }
        }

        public int TotalPostsCount(string userId)
        {
            using(var context = new DataContext())
            {
                return context.Posts.Where(i=>i.UserId==userId).Count();
            }
        }
    }
}