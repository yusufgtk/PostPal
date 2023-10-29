using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class FeedbackRepository : Repository<Feedback, DataContext>, IFeedbackRepository
    {
        public List<Feedback> GetFeedback()
        {
            using(var context = new DataContext())
            {
                return context.Feedbacks.ToList();
            }
        }

        public Feedback GetFeedbackById(int id)
        {
            using(var context = new DataContext())
            {
                return context.Feedbacks.Where(i=>i.Id==id).FirstOrDefault();
            }
        }

        public int TotalFeedbackCount()
        {
            using(var context = new DataContext())
            {
                return context.Feedbacks.Count();
            }
        }
    }
}