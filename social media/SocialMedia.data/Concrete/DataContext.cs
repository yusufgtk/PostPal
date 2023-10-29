using Microsoft.EntityFrameworkCore;
using SocialMedia.entity;

namespace SocialMedia.data.Concrete
{
    public class DataContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Save> Saves { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<BlueTickApplication> blueTickApplications { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\yusuf\\OneDrive\\Masaüstü\\social media\\database.db");
        }


    }
}