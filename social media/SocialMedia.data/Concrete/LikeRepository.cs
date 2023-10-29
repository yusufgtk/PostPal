using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class LikeRepository : Repository<Like, DataContext>, ILikeRepository
    {
        public List<Like> GetLikesByPostId(int postId)
        {
            using(var context  = new DataContext())
            {
                return context.Likes.Where(i=>i.PostId==postId).ToList();
            }
        }

        public Like GetLikeById(int id)
        {
            using(var context  = new DataContext())
            {
                return context.Likes.Where(i=>i.Id==id).FirstOrDefault();
            }
        }

        public int TotalLikesCountByPostId(int postId)
        {
            using(var context  = new DataContext())
            {
                return context.Likes.Where(i=>i.PostId==postId).Count();
            }
        }

        public bool IsLikePost(int postId, string userId)
        {
            using(var context  = new DataContext())
            {
                var isLike = context.Likes.Where(i=>i.PostId==postId&&i.UserId==userId).FirstOrDefault();
                if(isLike!=null)
                {
                    return true;
                }
                return false;
            }
        }

        public Like GetLikeByPostIdAndUserId(int postId, string userId)
        {
            using(var context  = new DataContext())
            {
                return context.Likes.Where(i=>i.PostId==postId&&i.UserId==userId).FirstOrDefault();
            }
        }
    }
}