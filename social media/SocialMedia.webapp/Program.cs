using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using morshop.app.Identity;
using SocialMedia.business.Abstract;
using SocialMedia.business.Concrete;
using SocialMedia.data.Abstract;
using SocialMedia.data.Concrete;
using SocialMedia.webapp.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(options=>options.UseSqlite("Data Source=C:\\Users\\yusuf\\OneDrive\\Masaüstü\\social media\\database.db"));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IPostRepository,PostRepository>();
builder.Services.AddScoped<IFollowerRepository,FollowerRepository>();
builder.Services.AddScoped<IFollowUpRepository,FollowUpRepository>();
builder.Services.AddScoped<ILikeRepository,LikeRepository>();
builder.Services.AddScoped<ICommentRepository,CommentRepository>();
builder.Services.AddScoped<INotificationRepository,NotificationRepository>();
builder.Services.AddScoped<ISaveRepository,SaveRepository>();
builder.Services.AddScoped<IMessageRepository,MessageRepository>();
builder.Services.AddScoped<IBlueTickApplicationRepository,BlueTickApplicationRepository>();
builder.Services.AddScoped<IStoryRepository,StoryRepository>();
builder.Services.AddScoped<IComplaintRepository,ComplaintRepository>();
builder.Services.AddScoped<IFeedbackRepository,FeedbackRepository>();

builder.Services.AddScoped<IPostServices,PostManager>();
builder.Services.AddScoped<IFollowerServices,FollowerManager>();
builder.Services.AddScoped<IFollowUpServices,FollowUpManager>();
builder.Services.AddScoped<ILikeServices,LikeManager>();
builder.Services.AddScoped<ICommentServices,CommentManager>();
builder.Services.AddScoped<INotificationServices,NotificationManager>();
builder.Services.AddScoped<ISaveServices,SaveManager>();
builder.Services.AddScoped<IMessageServices,MessageManager>();
builder.Services.AddScoped<IEmailServices,EmailManager>();
builder.Services.AddScoped<IBlueTickApplicationServices,BlueTickApplicationManager>();
builder.Services.AddScoped<IStoryServices,StoryManager>();
builder.Services.AddScoped<IComplaintServices,ComplaintManager>();
builder.Services.AddScoped<IFeedbackServices,FeedbackManager>();


builder.Services.Configure<IdentityOptions>(options=>{
        //Password
    options.Password.RequireDigit=true;//Parolanun içerisinde sayı olsun mu?
    options.Password.RequireLowercase=true;//Parolanun içerisinde küçük harf olsun mu?
    options.Password.RequireUppercase=false;//Parolanun içerisinde büyük harf olsun mu?
    options.Password.RequiredLength=6;//Parola Minimum kaç karakter olsun?
    options.Password.RequireNonAlphanumeric=false;//Parolanın içerisinde özel karakter olsun mu?

        //Lockout
    options.Lockout.AllowedForNewUsers=true;//Şifreyi yanlış girme sayısını doldurunca kilitlensin mi?
    options.Lockout.MaxFailedAccessAttempts=5;//Şifreyi en fazla kaçkere yanlış girebilsin?
    options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);//Kilitlenen hesap ne kadar süre sonra açılsın?

        //User
    //options.User.AllowedUserNameCharacters="";//User adında İstediğini karakterleri yazın?
    options.User.RequireUniqueEmail=true;//user ların e-mail adresleri benzersiz mi olsun?
    options.SignIn.RequireConfirmedEmail=false;//user mailini onaylatması zorunlu olsun mu?
    options.SignIn.RequireConfirmedPhoneNumber=false;//user telefon no onaylatması zorunlu olsun mu?


});
    //Cookie Ayarları
builder.Services.ConfigureExternalCookie(options=>{
    options.LoginPath="/account/login";//Kullanıcı login değilse nereye yonlendirilsin?
    options.LogoutPath="/account/logout";//Kullanıcı çıkış yaptığında nereye yönlendirilsin?
    options.AccessDeniedPath="/account/accessdenied";//Yekilendirme Admin,seller,costumer vb... Yani yetkisi hangi sayfaya yönlendirilsin?
    options.SlidingExpiration=false;//İstek yaptığında cookie süresi tazelensin mi?
    options.ExpireTimeSpan=TimeSpan.FromMinutes(60);//Cookie nekadar süre sonra silinsin? 
    //Güvenlik
    options.Cookie=new CookieBuilder
    {
        HttpOnly=true,//Sadece Http isteklerini kabul et
        Name=".MorShop.Security.Cookie",//Cookie nin Adı
        SameSite=SameSiteMode.Strict //bu özellik Cookie çalındığında başka bir cihazdan kullanılmasını önler
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "profile",
    pattern: "{controller=User}/{action=Profile}/{userId?}");
app.MapControllerRoute(
    name: "admin",
    pattern: "{controller=Admin}/{action=Dashboard}/{id?}");
    
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var userManager = services.GetRequiredService<UserManager<User>>();
var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
var configuration = services.GetRequiredService<IConfiguration>();
SeedIdentity.Seed(userManager, roleManager, configuration).Wait();

app.Run();
