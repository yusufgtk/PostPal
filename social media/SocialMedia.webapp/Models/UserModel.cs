namespace SocialMedia.webapp.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? Bio { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public int Posts { get; set; }
        public int Followers { get; set; }
        public int FollowUps { get; set; }
        public List<PostModel> PostModels { get; set; }
        // public List<FollowerModel> FollowerModels { get; set; }
        // public List<FollowUpModel> FollowUpModels { get; set; }
        
        public int PostModelsTotal()
        {
            int total=0;
            foreach(var model in PostModels)
            {
                total+=1;
            }
            return total;
        }
        // public int FollowerModelsTotal()
        // {
        //     int total=0;
        //     foreach(var model in FollowerModels)
        //     {
        //         total+=1;
        //     }
        //     return total;
        // }
        // public int FollowModelsTotal()
        // {
        //     int total=0;
        //     foreach(var model in FollowUpModels)
        //     {
        //         total+=1;
        //     }
        //     return total;
        // }
    }
}