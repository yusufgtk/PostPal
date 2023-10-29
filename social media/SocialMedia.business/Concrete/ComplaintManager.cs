using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.business.Concrete
{
    public class ComplaintManager : IComplaintServices
    {
        private readonly IComplaintRepository _complaintRepository;
        public ComplaintManager(IComplaintRepository complaintRepository)
        {
            _complaintRepository=complaintRepository;
        }
        public void Create(Complaint entity)
        {
            _complaintRepository.Create(entity);
        }

        public void Delete(Complaint entity)
        {
            _complaintRepository.Delete(entity);
        }

        public void DeleteMultiple(List<Complaint> entities)
        {
            _complaintRepository.DeleteMultiple(entities);
        }

        public bool DidYouComplaint(string userId, int postId)
        {
            return _complaintRepository.DidYouComplaint(userId,postId);
        }

        public List<Complaint> GetComplaint()
        {
            return _complaintRepository.GetComplaints();
        }

        public List<Complaint> GetComplaintsByPostId(int postId)
        {
            return _complaintRepository.GetComplaintsByPostId(postId);
        }

        public int TotalComplaintsCount()
        {
            return _complaintRepository.TotalComplaintsCount();
        }

        public int TotalComplaintsCountByPost(int postId)
        {
            return _complaintRepository.TotalComplaintsCountByPost(postId);
        }

        public void Update(Complaint entity)
        {
            _complaintRepository.Update(entity);
        }
    }
}