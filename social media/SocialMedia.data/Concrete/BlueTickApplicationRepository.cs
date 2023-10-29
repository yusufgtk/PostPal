using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class BlueTickApplicationRepository : Repository<BlueTickApplication, DataContext>, IBlueTickApplicationRepository
    {
        public void DeleteReferences()
        {
            //1000 takipçinin altındaki başvuruları sil
            using(var context = new DataContext())
            {
                var applications = context.blueTickApplications.ToList();
                foreach(var item in applications)
                {   
                    var followersCount = context.Followers.Where(i=>i.UserId==item.UserId).Count();
                    if(followersCount==1000)
                    {
                        using(var context2 = new DataContext())
                        {
                            context2.blueTickApplications.Remove(item);
                            context2.SaveChanges();
                        }
                        
                    }
                }
                
            }
        }

        public BlueTickApplication GetBlueTickApplicationById(int id)
        {
            using(var context = new DataContext())
            {
                return context.blueTickApplications.Where(i=>i.Id==id).FirstOrDefault();
            }
        }

        public BlueTickApplication GetBlueTickApplicationByUserId(string userId)
        {
            using(var context = new DataContext())
            {
                return context.blueTickApplications.Where(i=>i.UserId==userId).FirstOrDefault();
            }
        }

        public List<BlueTickApplication> GetBlueTickApplications()
        {
            using(var context = new DataContext())
            {
                return context.blueTickApplications.OrderByDescending(i=>i.Description).Take(50).ToList();
            }
        }
    }
}