using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class SaveRepository : Repository<Save, DataContext>, ISaveRepository
    {
        public Save GetSaveByPostIdAndUserId(int postId, string userId)
        {
            using(var context = new DataContext())
            {
                return context.Saves.Where(i=>i.UserId==userId&&i.PostId==postId).FirstOrDefault();
            }
        }

        public List<Save> GetSavesByPostId(int postId)
        {
            using(var context = new DataContext())
            {
                return context.Saves.Where(i=>i.PostId==postId).ToList();
            }
        }

        public List<Save> GetSavesByUserId(string userId)
        {
            using(var context = new DataContext())
            {
                return context.Saves.Where(i=>i.UserId==userId).OrderByDescending(i=>i.CreatedAt).ToList();
            }
        }

        public bool IsSavePost(int postId, string userId)
        {
            using(var context  = new DataContext())
            {
                var isSave = context.Saves.Where(i=>i.PostId==postId&&i.UserId==userId).FirstOrDefault();
                if(isSave!=null)
                {
                    return true;
                }
                return false;
            }
        }

        public int TotalSavesCount(string userId)
        {
            using(var context = new DataContext())
            {
                return context.Saves.Where(i=>i.UserId==userId).Count();
            }
        }
    }
}