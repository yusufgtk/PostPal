using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface IComplaintRepository : IRepository<Complaint>
    {
        List<Complaint> GetComplaints();
        List<Complaint> GetComplaintsByPostId(int postId);
        bool DidYouComplaint(string userId, int postId);
        int TotalComplaintsCount();
        int TotalComplaintsCountByPost(int postId);
    }
}