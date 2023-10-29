namespace SocialMedia.webapp.Models
{
    public class DashboardModel
    {
        public int Id { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfBlueTickApplications { get; set; }
        public int NumberOfComplaints { get; set; }
        public int NumberOfFeedbacks { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public IList<string> Roles { get; set; }

    }
}