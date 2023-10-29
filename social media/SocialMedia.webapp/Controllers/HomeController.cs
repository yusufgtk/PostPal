using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.entity;
using SocialMedia.webapp.Identity;
using SocialMedia.webapp.Models;

namespace SocialMedia.webapp.Controllers;

[Authorize(Roles ="User")]
public class HomeController : Controller
{
    private readonly IPostServices _postService;
    private readonly ILikeServices _likeServices;
    private readonly ICommentServices _commentServices;
    private readonly IFollowerServices _followerServices;
    private readonly IFollowUpServices _followUpServices;
    private readonly ISaveServices _saveService;
    private readonly INotificationServices _notificationServices;
    private readonly IStoryServices _storyServices;
    private readonly IFeedbackServices _feedbackServices;
    private readonly UserManager<User> _userManager;
    public HomeController(IPostServices postService, ILikeServices likeServices, ICommentServices commentService, IFollowerServices followerServices, IFollowUpServices followUpServices, ISaveServices saveService, INotificationServices notificationServices, IStoryServices storyServices, IFeedbackServices feedbackServices, UserManager<User> userManager)
    {
        _postService=postService;
        _likeServices=likeServices;
        _commentServices=commentService;
        _followerServices=followerServices;
        _followUpServices=followUpServices;
        _saveService=saveService;
        _notificationServices=notificationServices;
        _storyServices=storyServices;
        _feedbackServices=feedbackServices;
        _userManager=userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        Console.WriteLine("userid"+userId);
        var posts = _postService.GetHomePosts(userId);
        var postModels = new List<PostModel>(){};
        foreach(var post in posts)
        {
            var user = await _userManager.FindByIdAsync(post.UserId);
            postModels.Add(new PostModel(){
                Id=post.Id,
                Content=post.Content,
                ImageUrl=post.ImageUrl,
                IsApproved=user.IsApproved,
                IsLike=_likeServices.IsLikePost(post.Id,userId),
                IsSave=_saveService.IsSavePost(post.Id,userId),
                UserId=post.UserId,
                UserName=post.UserName,
                CreatedAt=post.CreatedAt,
                IsCommentOpen=post.IsCommentOpen,
                LikesCount=_likeServices.TotalLikesCountByPostId(post.Id),
                CommentsCount=_commentServices.TotalCommentsCountByPostId(post.Id)
            });
        }
        postModels=postModels.OrderByDescending(i=>i.CreatedAt).ToList();
        var users = _userManager.Users.Where(i=>i.IsApproved==true).Take(10).ToList();
        ViewBag.DiscoverUsers=users;
        ViewBag.Likes = new List<UserModel>(){};
        var followUps=_followUpServices.GetFollowUps(userId);
        var haveStory = new List<UserModel>(){};
        var StoryModels = new List<StoryModel>(){};
        foreach(var item in followUps)
        {
            if(_storyServices.IsThereStory(item.FollowUpUserId))
            {
                var user = await _userManager.FindByIdAsync(item.FollowUpUserId);
                haveStory.Add(new UserModel(){
                    Id=user.Id,
                    UserName=user.UserName,
                    IsApproved=user.IsApproved
                });
            }
            if(_storyServices.IsThereStory(_userManager.GetUserId(User)))
            {
                ViewBag.IHaveStory = "true";
            }
        }
        foreach (var item in haveStory)
        {
            var stories=_storyServices.GetStories24HoursByFollowUpUserId(item.Id);
            foreach (var story in stories)
            {
                StoryModels.Add(new StoryModel(){
                    Id=story.Id,
                    UserId=story.UserId,
                    UserName=story.UserName,
                    ImageUrl=story.ImageUrl,
                    Description=story.Description,
                    CreatedAt=story.CreatedAt
                });
            }
        }
        ViewBag.IStories=_storyServices.GetStories24HoursByFollowUpUserId(_userManager.GetUserId(User));
        ViewBag.User=await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        ViewBag.HaveStory=haveStory;
        ViewBag.Stories=StoryModels;
        return View(postModels);

    }


    public IActionResult Discover()
    {
        var discoverPosts=_postService.GetDiscoverPosts();
        var postModels = new List<PostModel>(){};
        foreach(var post in discoverPosts)
        {
            postModels.Add(new PostModel(){
                Id=post.Id,
                Content=post.Content,
                ImageUrl=post.ImageUrl,
                UserId=post.UserId,
                UserName=post.UserName,
                CreatedAt=post.CreatedAt,
                IsCommentOpen=post.IsCommentOpen,
                LikesCount=_likeServices.TotalLikesCountByPostId(post.Id),
                CommentsCount=_commentServices.TotalCommentsCountByPostId(post.Id)
            });
        }
        postModels=postModels.OrderByDescending(i=>i.LikesCount).ToList().OrderByDescending(i=>i.CommentsCount).ToList();
        return View(postModels);
    }

    public async Task<IActionResult> Notifications()
    {
        var notifications=_notificationServices.GetNotificationByUserId(_userManager.GetUserId(User));
        var notificationModels=new List<NotificationModel>(){};
        foreach (var n in notifications)
        {
            var user = await _userManager.FindByIdAsync(n.UserId2);
            notificationModels.Add(new NotificationModel(){
                UserId=n.UserId,
                UserId2=n.UserId2,
                UserName=user.UserName,
                PostId=n.PostId,
                NotificationType=n.NotificationType,
                Content=n.Content,
                CreatedAt=n.CreatedAt,
                Seen=n.Seen
            });
        }
        foreach(var n in notifications)
        {
            n.Seen=true;
            _notificationServices.Update(n);
        }
        return View(notificationModels);
    }
    public async Task<IActionResult> PostDetails(int postId)
    {
        var post=_postService.GetPostById(postId);
        if(post==null)
        {
            return NotFound();
        }
        var user = await _userManager.FindByIdAsync(post.UserId);
        var postModel = new PostModel(){
            Id=post.Id,
            Content=post.Content,
            ImageUrl=post.ImageUrl,
            IsApproved=user.IsApproved,
            IsLike=_likeServices.IsLikePost(post.Id,_userManager.GetUserId(User)),
            IsSave=_saveService.IsSavePost(post.Id,_userManager.GetUserId(User)),
            UserId=post.UserId,
            UserName=post.UserName,
            CreatedAt=post.CreatedAt,
            IsCommentOpen=post.IsCommentOpen,
            LikesCount=_likeServices.TotalLikesCountByPostId(post.Id),
            CommentsCount=_commentServices.TotalCommentsCountByPostId(post.Id)
        };
        return View(postModel);
    }

    ///likes
    [HttpGet]
    public async Task<IActionResult> Likes(int postId)
    {
        ViewBag.Likes = new List<UserModel>(){};
        var post=_postService.GetPostById(postId);
        var user = await _userManager.FindByIdAsync(post.UserId);
        if(post!=null && user!=null)
        {
            var userModels = new List<UserModel>(){};
            var likes=_likeServices.GetLikesByPostId(postId);
            if(likes!=null)
            {
                foreach(var like in likes)
                {
                    var userLike = await _userManager.FindByIdAsync(like.UserId);
                    userModels.Add(new UserModel(){
                        Id=like.UserId,
                        UserName=userLike.UserName,
                        ImageUrl=userLike.ImageUrl
                    });
                }
            }
            return View("PostLikes",userModels);
        }
        return NotFound();
    }
    [HttpPost]
    public IActionResult Like(int postId, string posturl)
    {
        var  userId = _userManager.GetUserId(User);
        if(_likeServices.IsLikePost(postId,userId))
        {
            var like = _likeServices.GetLikeByPostIdAndUserId(postId,userId);
            if(like != null)
            {
                _likeServices.Delete(like);
                _notificationServices.DeleteNotification(postId,userId);
            }
            
        }
        else
        {
            var like = new Like();
            like.UserId=userId;
            like.PostId=postId;
            _likeServices.Create(like);
            var post = _postService.GetPostById(postId);
            var notification = new Notification(){
                UserId=post.UserId,
                UserId2=userId,
                PostId=postId,
                NotificationType="Like",
                Content="Kişisi gönderini beğendi.",
                CreatedAt=DateTime.Now,
                Seen=false
            };
            _notificationServices.Create(notification);
        }
        var requestUrl = HttpContext.Request.Path;
        Console.WriteLine("-----------------------------------");
        Console.WriteLine(posturl);
        
        if(posturl=="/Home/Index"||posturl=="/Home"||posturl=="/"||posturl.Contains("/Home/Index/"))
        {
            return Redirect("/Home/Index/#"+postId.ToString());
        }
        else{
            return RedirectToAction("PostDetails","Home", new { postId = postId });
        }
        
    }
    [HttpPost]
    public async Task<IActionResult> SaveProccess(int postId, string postUrl)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        var post = _postService.GetPostById(postId);
        if(post==null)
        {
            return NotFound();
        }
        if(_saveService.IsSavePost(post.Id,user.Id))
        {
            var save = _saveService.GetSaveByPostIdAndUserId(post.Id,user.Id);
            if(save!=null)
            {
                _saveService.Delete(save);
            }
            
        }
        else
        {
            var save =new Save(){
                UserId = user.Id,
                UserName=user.UserName,
                PostId=post.Id,
                ImageUrl=post.ImageUrl,
                CreatedAt=DateTime.Now
            };
            _saveService.Create(save);
        }
        if(postUrl=="/Home/Index"||postUrl=="/Home"||postUrl=="/"||postUrl.Contains("/Home/Index/"))
        {
            return Redirect("/Home/Index/#"+postId.ToString());
        }
        else{
            return RedirectToAction("PostDetails","Home", new { postId = postId });
        }
    }
    public async Task<IActionResult> Comments(int postId)
    {
        var comments=_commentServices.GetCommentsByPostId(postId);
        var commentModels = new List<CommentModel1>(){};
        foreach(var c in comments)
        {
            var user=await _userManager.FindByIdAsync(c.UserId);
            commentModels.Add(new CommentModel1(){
                Id=c.Id,
                UserId=c.UserId,
                UserName=user.UserName,
                PostId=c.PostId,
                Content=c.Content
            });
        }
        return View("PostComments",commentModels);
    }
    [HttpPost]
    public IActionResult Comment(int postId, string comment, string postUrl)
    {
        if(comment!=null)
        {
            var comment1 = new Comment(){
                PostId=postId,
                UserId=_userManager.GetUserId(User),
                Content=comment
            };
            _commentServices.Create(comment1);
            var post = _postService.GetPostById(postId);
            var notification = new Notification(){
                UserId=post.UserId,
                UserId2=_userManager.GetUserId(User),
                PostId=postId,
                NotificationType="Comment",
                Content=comment,
                CreatedAt=DateTime.Now,
                Seen=false
            };
            _notificationServices.Create(notification);
            
        }
        if(postUrl=="/Home/Index"||postUrl=="/Home"||postUrl=="/"||postUrl.Contains("/Home/Index/"))
        {
            return Redirect("/Home/Index/#"+postId.ToString());
        }
        else{
            return RedirectToAction("PostDetails","Home", new { postId = postId });
        }
    }

    public async Task<IActionResult> FollowUps(string userId)
    {
        var followUps=_followUpServices.GetFollowUps(userId);
        var userModels=new List<UserModel>(){};
        foreach(var f in followUps)
        {
            var user=await _userManager.FindByIdAsync(f.FollowUpUserId);
            userModels.Add(new UserModel(){
                Id=user.Id,
                UserName=user.UserName,
                Bio=user.Bio,
                ImageUrl=user.ImageUrl,
                IsApproved=user.IsApproved,
            });
        }
        ViewBag.UserId=userId;
        return View(userModels);
    }
    [HttpPost]
    public async Task<IActionResult> FollowUps(string search, string userId)
    {
        var followUps=_followUpServices.GetFollowUps(userId);
        var userModels=new List<UserModel>(){};
        if(search==null)
        {
            foreach(var f in followUps)
            {
                var user=await _userManager.FindByIdAsync(f.FollowUpUserId);
                userModels.Add(new UserModel(){
                    Id=user.Id,
                    UserName=user.UserName,
                    Bio=user.Bio,
                    ImageUrl=user.ImageUrl,
                    IsApproved=user.IsApproved,
                });
            }
        }
        else
        {
            foreach(var f in followUps)
            {
                var user=await _userManager.FindByIdAsync(f.FollowUpUserId);
                if(user.UserName.Contains(search))
                {
                    userModels.Add(new UserModel(){
                        Id=user.Id,
                        UserName=user.UserName,
                        Bio=user.Bio,
                        ImageUrl=user.ImageUrl,
                        IsApproved=user.IsApproved,
                    });
                }
            }
        }
        ViewBag.UserId=userId;
        return View(userModels);
    }
    public async Task<IActionResult> Followers(string userId)
    {
        var followers=_followerServices.GetFollowers(userId);
        var userModels=new List<UserModel>(){};
        foreach(var f in followers)
        {
            var user=await _userManager.FindByIdAsync(f.FollowerUserId);
            if(user!=null)
            {
                userModels.Add(new UserModel(){
                    Id=user.Id,
                    UserName=user.UserName,
                    Bio=user.Bio,
                    ImageUrl=user.ImageUrl,
                    IsApproved=user.IsApproved,
                });
            }
            
        }
        ViewBag.UserId=userId;
        return View(userModels);
    }
    [HttpPost]
    public async Task<IActionResult> Followers(string search, string userId)
    {
        var followers=_followerServices.GetFollowers(userId);
        var userModels=new List<UserModel>(){};
        if(search==null)
        {
            foreach(var f in followers)
            {
                var user=await _userManager.FindByIdAsync(f.FollowerUserId);
                userModels.Add(new UserModel(){
                    Id=user.Id,
                    UserName=user.UserName,
                    Bio=user.Bio,
                    ImageUrl=user.ImageUrl,
                    IsApproved=user.IsApproved,
                });
            }
        }
        else
        {
            foreach(var f in followers)
            {
                
                var user=await _userManager.FindByIdAsync(f.FollowerUserId);
                if(user.UserName.Contains(search))
                {
                    userModels.Add(new UserModel(){
                        Id=user.Id,
                        UserName=user.UserName,
                        Bio=user.Bio,
                        ImageUrl=user.ImageUrl,
                        IsApproved=user.IsApproved,
                    });
                }
                
            }
        }
        if(userModels==null)
        {
            TempData["Messages"]="Kullanıcı Bulunamadı!";
        }
        ViewBag.UserId=userId;
        return View(userModels);
    }

    public IActionResult Feedback()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Feedback([FromForm] IFormFile file, [FromForm] string content)
    {
        if(content==null)
        {
            TempData["Message"]="Açıklamayı boş bırakmazsın!";
            TempData["Alert"]="alert-danger";
            return View();
        }
        TempData["Message"]="Geri bildiriminiz iletildi.";
        TempData["Alert"]="alert-success";
        var feedback = new Feedback(){
            UserId=_userManager.GetUserId(User),
            Content=content,
            CreatedAt=DateTime.Now
        };
        if(file!=null&&file.Length!=0)
        {
            var fileName=Guid.NewGuid()+".jpeg";
            var path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","feedbackImages",fileName);
            using(var stream=new FileStream(path,FileMode.Create))
            {
                file.CopyTo(stream);
            }
            feedback.ImageUrl=fileName;
        }
        else
        {
            feedback.ImageUrl=null;
        }
        
        _feedbackServices.Create(feedback);
        return View();
    }
    public IActionResult Error()
    {
        return View();
    }
    
    
}
