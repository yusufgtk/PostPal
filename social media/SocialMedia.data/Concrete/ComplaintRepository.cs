using SocialMedia.data.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class ComplaintRepository : Repository<Complaint, DataContext>, IComplaintRepository
    {
        public bool DidYouComplaint(string userId, int postId)
        {
            using(var context = new DataContext())
            {
                return context.Complaints.Any(i=>i.UserId==userId&&i.PostId==postId);

            }
        }

        public List<Complaint> GetComplaints()
        {
            using(var context = new DataContext())
            {
                var groupedComplaints = context.Complaints
                    .GroupBy(i => i.PostId) // PostId'ye göre grupla
                    .Select(group => group.First()) // Her gruptan ilk öğeyi seç
                    .ToList();

                return groupedComplaints;
            }
        }

        public List<Complaint> GetComplaintsByPostId(int postId)
        {
            using(var context = new DataContext())
            {
                return context.Complaints.Where(i=>i.PostId==postId).ToList();
            }
        }

        public int TotalComplaintsCount()
        {
            using(var context = new DataContext())
            {
                return context.Complaints.Count();
            }
        }

        public int TotalComplaintsCountByPost(int postId)
        {
            using(var context = new DataContext())
            {
                return context.Complaints.Where(i=>i.PostId==postId).Count();
            }
        }
    }
}