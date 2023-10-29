using SocialMedia.entity;

namespace SocialMedia.business.Abstract
{
    public interface IComplaintServices
    {
        void Create(Complaint entity);
        void Delete(Complaint entity);
        void DeleteMultiple(List<Complaint> entities);
        void Update(Complaint entity);
        List<Complaint> GetComplaint();
        List<Complaint> GetComplaintsByPostId(int postId);
        bool DidYouComplaint(string userId, int postId);
        int TotalComplaintsCount();
        int TotalComplaintsCountByPost(int postId);
    }
}