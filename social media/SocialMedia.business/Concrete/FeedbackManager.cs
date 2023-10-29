using SocialMedia.business.Abstract;
using SocialMedia.data.Concrete;
using SocialMedia.entity;

namespace SocialMedia.business.Concrete
{
    public class FeedbackManager : IFeedbackServices
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackManager(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository=feedbackRepository;
        }
        public void Create(Feedback entity)
        {
            _feedbackRepository.Create(entity);
        }

        public void Delete(Feedback entity)
        {
            _feedbackRepository.Delete(entity);
        }

        public List<Feedback> GetFeedback()
        {
            return _feedbackRepository.GetFeedback();
        }

        public Feedback GetFeedbackById(int id)
        {
            return _feedbackRepository.GetFeedbackById(id);
        }

        public int TotalFeedbackCount()
        {
            return _feedbackRepository.TotalFeedbackCount();
        }

        public void Update(Feedback entity)
        {
            _feedbackRepository.Update(entity);
        }
    }
}