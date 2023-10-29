using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        List<Feedback> GetFeedback();
        Feedback GetFeedbackById(int id);
        int TotalFeedbackCount();
    }
}