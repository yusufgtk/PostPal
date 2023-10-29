using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class CommentRepository : Repository<Comment, DataContext>, ICommentRepository
    {
        public Comment GetCommentById(int id)
        {
            using(var context  = new DataContext())
            {
                return context.Comments.Where(i=>i.Id==id).FirstOrDefault();
            }
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            using(var context  = new DataContext())
            {
                return context.Comments.Where(i=>i.PostId==postId).ToList();
            }
        }

        public int TotalCommentsCountByPostId(int postId)
        {
            using(var context  = new DataContext())
            {
                return context.Comments.Where(i=>i.PostId==postId).Count();
            }
        }
    }
}