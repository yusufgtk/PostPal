using Microsoft.AspNetCore.Identity;
using SocialMedia.webapp.Identity;

namespace morshop.app.Identity
{
    public static class SeedIdentity
    {
        //IConfiguration ile appsetting üzerindeki bilgileri almamızı sağlar
        public static async Task Seed(UserManager<User> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            var username=configuration["Data:AdminUser:username"];
            var password=configuration["Data:AdminUser:password"];
            var email=configuration["Data:AdminUser:email"];
            var role=configuration["Data:AdminUser:role"];
            var imageUrl=configuration["Data:AdminUser:imageUrl"];

            if(await userManager.FindByNameAsync(username)==null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                var user = new User{
                    FirstName="admin",
                    LastName="admin",
                    UserName=username,
                    Email=email,
                    EmailConfirmed=true,
                    ImageUrl=imageUrl
                };
                var result = await userManager.CreateAsync(user,password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,role);
                    var role2 = new IdentityRole(){
                        Name="User",
                        NormalizedName="USER"
                    };
                    await roleManager.CreateAsync(role2);
                }
            }
        }
    }


}