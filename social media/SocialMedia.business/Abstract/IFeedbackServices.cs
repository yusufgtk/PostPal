using SocialMedia.entity;

namespace SocialMedia.business.Abstract
{
    public interface IFeedbackServices
    {
        void Create(Feedback entity);
        void Delete(Feedback entity);
        void Update(Feedback entity);
        List<Feedback> GetFeedback();
        Feedback GetFeedbackById(int id);
        int TotalFeedbackCount();
    }
}