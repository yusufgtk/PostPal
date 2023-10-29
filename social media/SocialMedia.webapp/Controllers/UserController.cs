using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.entity;
using SocialMedia.webapp.Identity;
using SocialMedia.webapp.Models;

namespace SocialMedia.webapp.Controllers
{
    [Authorize(Roles ="User")]
    public class UserController : Controller
    {
        private readonly IPostServices _postService;
        private readonly IFollowerServices _followerServices;
        private readonly IFollowUpServices _followUpServices;
        private readonly INotificationServices _notificationServices;
        private readonly ISaveServices _saveService;
        private readonly IMessageServices _messageServices;
        private readonly IBlueTickApplicationServices _blueTickApplicationServices;
        private readonly IStoryServices _storyServices;
        private readonly IComplaintServices _complaintServices;
        private readonly UserManager<User> _userManager;
        public UserController(IPostServices postService, IFollowerServices followerServices, IFollowUpServices followUpServices, INotificationServices notificationServices, ISaveServices saveService, IMessageServices messageServices, 
        IBlueTickApplicationServices blueTickApplicationServices, IStoryServices storyServices, IComplaintServices complaintServices, UserManager<User> userManager)
        {
            _postService=postService;
            _followerServices=followerServices;
            _followUpServices=followUpServices;
            _notificationServices=notificationServices;
            _saveService=saveService;
            _messageServices=messageServices;
            _blueTickApplicationServices=blueTickApplicationServices;
            _storyServices=storyServices;
            _complaintServices=complaintServices;
            _userManager=userManager;
        }
        public IActionResult Settings()
        {
            ViewBag.UserName=_userManager.GetUserName(User);
            return View();
        }
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if(user == null)
            {
                return Redirect("Home/Error");
            }
            var profilModel= new ProfileModel(){
                UserId=user.Id,
                UserName=user.UserName,
                Bio=user.Bio
            };
            return View(profilModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile([FromForm] ProfileModel profileModel, [FromForm] string userId, [FromForm] IFormFile file)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return Redirect("Home/Error");
            }

            if(file!=null && file.Length!=0)
            {
                if(System.IO.File.Exists("~/userImages/"+user.UserName.ToString()+".jpeg"))
                {
                    System.IO.File.Delete("~/userImages/"+user.UserName.ToString()+".jpeg");
                }
                else
                {
                    TempData["Message"]="Resim bulunamadı.";
                }
                var fileName = profileModel.UserName+".jpeg";
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","userImages",fileName);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                user.ImageUrl=fileName;
            }
            user.UserName=profileModel.UserName;
            user.Bio=profileModel.Bio;
            await _userManager.UpdateAsync(user);
            TempData["Message"]="İşleminiz başarıyla gerçekleştirildi!";
            TempData["Alert"]="alert-success";

            return RedirectToAction("Settings");
        }
        public async Task<IActionResult> IProfile()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if(user!=null)
            {
                var userModel = new UserModel(){
                    Id=user.Id,
                    UserName=user.UserName,
                    Bio=user.Bio,
                    ImageUrl=user.ImageUrl,
                    IsApproved=user.IsApproved,
                    Posts=_postService.TotalPostsCount(user.Id),
                    Followers=_followerServices.TotalFollowerCount(user.Id),
                    FollowUps=_followUpServices.TotalFollowUpCount(user.Id)
                };
                ViewBag.IPosts=_postService.GetPostsByUserId(user.Id);
                return View(userModel);
            }
            return Redirect("Home/Error");
        }
        public async Task<IActionResult> Profile(string userId)
        {
            
            if(userId==null)
            {
                return Redirect("Home/Error");
            }
            if(userId==_userManager.GetUserId(User))
            {
                return RedirectToAction("IProfile");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(user!=null)
            {
                var userModel = new UserModel(){
                    Id=user.Id,
                    UserName=user.UserName,
                    Bio=user.Bio,
                    ImageUrl=user.ImageUrl,
                    IsApproved=user.IsApproved,
                    IsActive=user.IsActive,
                    Posts=_postService.TotalPostsCount(user.Id),
                    Followers=_followerServices.TotalFollowerCount(user.Id),
                    FollowUps=_followUpServices.TotalFollowUpCount(user.Id)
                    
                };
                ViewBag.Posts=_postService.GetPostsByUserId(user.Id);
                var followUps=_followUpServices.GetFollowUps(_userManager.GetUserId(User));
                foreach (var item in followUps)
                {
                    if(item.FollowUpUserId==userId)
                    {
                        ViewBag.FollowUp=1;
                        return View(userModel);
                    }
                }
                ViewBag.FollowUp=0;
                return View(userModel);
            }
            return Redirect("Home/Error");
            
        }
        public async Task<IActionResult> FindUserName([FromForm] string search)
        {
            if(search==_userManager.GetUserName(User)||search==null)
            {
                return Redirect("/Home/Discover");
            }

            var user = await _userManager.FindByNameAsync(search);
            if(user!=null)
            {
                return RedirectToAction("Profile", new { userId = user.Id });
            }
            else
            {
                TempData["Message"]=search+" kullanıcı bulunamadı!";
                TempData["Alert"]="alert-danger";
                return Redirect("/home/discover");
            }
        }
        public async Task<IActionResult> FollowUpProccess([FromForm] string userId, [FromForm] string followUpState)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(followUpState=="false")
            {
                var followUp = new FollowUp(){
                UserId=_userManager.GetUserId(User),
                FollowUpUserId=userId
                };
                var follower = new Follower(){
                    UserId=userId,
                    FollowerUserId=_userManager.GetUserId(User)
                };

                _followUpServices.Create(followUp);
                _followerServices.Create(follower);
                //ViewBag.FollowUp=1;
                var notification = new Notification(){
                    UserId=followUp.FollowUpUserId,
                    UserId2=_userManager.GetUserId(User),
                    NotificationType="Follow",
                    Content="Seni artık takip ediyor.",
                    CreatedAt=DateTime.Now,
                    Seen=false
                };
                _notificationServices.Create(notification);
                ViewData["Message"] = user.UserName+" kişisini artık takip ediyorsunuz.";
                ViewData["Alert"] = "alert-success";
            }
            else
            {
                var followUp = _followUpServices.GetFollowUpByUserId(_userManager.GetUserId(User),userId);
                var follower = _followerServices.GetFollowerByUserId(userId,_userManager.GetUserId(User));
                if(followUp != null && follower != null)
                {
                    _followUpServices.Delete(followUp);
                    _followerServices.Delete(follower);
                    _notificationServices.DeleteNotification(follower.UserId,followUp.UserId);
                    ViewData["Message"] = user.UserName+" kişisini takipten çıktınız!";
                    ViewData["Alert"] = "alert-danger";
                    ViewBag.FollowUp=0;
                }
                
            }
            
            
            return RedirectToAction("Profile", new { userId = user.Id });
        }

        
        // /////////////////////////////////////////////////////////// 
        public IActionResult Saves()
        {
            var userId=_userManager.GetUserId(User);
            var saves=_saveService.GetSavesByUserId(userId);
            var saveModels=new List<SaveModel>(){};
            foreach(var save in saves)
            {
                saveModels.Add(new SaveModel(){
                    Id = save.Id,
                    UserId=userId,
                    PostId=save.PostId,
                    ImageUrl=save.ImageUrl,
                    CreatedAt=save.CreatedAt
                });
            }
            return View(saveModels);
        }
        ////////////////////////////////////////////////////
        public IActionResult AddPost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPost([FromForm] IFormFile file, [FromForm] AddPostModel addPostModel)
        {
            if(!ModelState.IsValid)
            {
                return View(addPostModel);
            }
            if(file!=null&&file.Length!=0)
            {
                var fileName=Guid.NewGuid()+".jpeg";
                var path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","postImages",fileName);
                using(var stream=new FileStream(path,FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                Console.WriteLine("---------------------------");
                Console.WriteLine(addPostModel.IsCommentOpen);
                Console.WriteLine("---------------------------");
                var post = new Post(){
                    UserId=_userManager.GetUserId(User),
                    UserName=_userManager.GetUserName(User),
                    Content=addPostModel.Content,
                    CreatedAt=DateTime.Now,
                    ImageUrl=fileName,
                    IsCommentOpen=addPostModel.IsCommentOpen
                };
                _postService.Create(post);
            }
            
            return Redirect("/");
        }
        //-----------------------------------------------------------------------
        public async Task<ActionResult> ChatList()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var followUps = _followUpServices.GetFollowUps(user.Id);
            var followers = _followerServices.GetFollowers(user.Id);
            var userModels=new List<UserModel>(){};
            foreach(var followUp in followUps)
            {
                foreach(var follower in followers)
                {
                    if(followUp.FollowUpUserId==follower.FollowerUserId)
                    {
                        var user2=await _userManager.FindByIdAsync(followUp.FollowUpUserId);
                        userModels.Add(new UserModel(){
                            Id=followUp.FollowUpUserId,
                            UserName=user2.UserName,
                            IsApproved=user2.IsApproved
                        });
                    }
                }
                Console.WriteLine(followUp.FollowUpUserId);
            }
            return View(userModels);
        }
        public async Task<IActionResult> Chat(string userId)
        {
            
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if(userId==null||user==null)
            {
                return Redirect("Home/Error");
            }
            var receiverUser=await _userManager.FindByIdAsync(userId);
            var messageModels=new List<MessageModel>(){};
            var receiverMessages=_messageServices.GetMessagesBySenderIdAndReceiverId(userId,user.Id);
            var senderMessages=_messageServices.GetMessagesBySenderIdAndReceiverId(user.Id,userId);
            foreach(var message in receiverMessages)
            {
                messageModels.Add(new MessageModel(){
                    Id=message.Id,
                    Content=message.Content,
                    CreatedAt=message.CreatedAt,
                    Who=false
                });
            }
            foreach(var message in senderMessages)
            {
                messageModels.Add(new MessageModel(){
                    Id=message.Id,
                    Content=message.Content,
                    CreatedAt=message.CreatedAt,
                    Who=true
                });
            }

            ViewBag.SenderUser=user.UserName;
            ViewBag.ReceiverUser=receiverUser;
            messageModels=messageModels.OrderBy(i=>i.CreatedAt).ToList();
            return View(messageModels);
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(string messageContent, string receiverUserId)
        {
            var sender=await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var receiver=await _userManager.FindByIdAsync(receiverUserId);
            if(receiver==null)
            {
                return Redirect("Home/Error");
            }
            var message = new Message(){
                SenderUserId=sender.Id,
                ReceiverUserId=receiver.Id,
                Content=messageContent,
                CreatedAt=DateTime.Now
            };
            _messageServices.Create(message);


            return RedirectToAction("Chat",new {userId=receiverUserId});
        }

        public IActionResult ResetPassword()
        {
            var resetPasswordModel = new ResetPasswordModel();
            return View(resetPasswordModel);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordModel resetPasswordModel)
        {
            if(!ModelState.IsValid)
            {
                return View(resetPasswordModel);
            }
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if(user == null)
            {
                return Redirect("/home/error");
            }
            bool result = await _userManager.CheckPasswordAsync(user, resetPasswordModel.OldPassword);
            if(result)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result2 =  await _userManager.ResetPasswordAsync(user, token, resetPasswordModel.Password);
                if(result2.Succeeded)
                {
                    TempData["Message"] = "Şifreniz Güncellendi!";
                    TempData["Alert"] = "alert-success";
                    return Redirect("/user/settings");
                }
            }
            else
            {
                TempData["Message"] = "Şuanki şifrenizi yanlış girdiniz!";
                TempData["Alert"] = "alert-danger";
            }
            return View();
        }

        public async Task<IActionResult> BlueTick()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if(user.IsApproved)
            {
                ViewBag.isBlueTickApplication=2; // başvuru onaylı
                return View();
            }
            var BlueTickApplication = _blueTickApplicationServices.GetBlueTickApplicationByUserId(_userManager.GetUserId(User));
            if(BlueTickApplication==null)
            {
                ViewBag.isBlueTickApplication=0; // başvuru yok
                return View();
            }
            var BlueTickApplicationModel = new BlueTickApplication(){
                Id=BlueTickApplication.Id,
                UserId=BlueTickApplication.UserId,
                Description=BlueTickApplication.Description,
                IsApplication=BlueTickApplication.IsApplication
            };
            ViewBag.isBlueTickApplication=1; // başvuru yapıldı
            return View(BlueTickApplicationModel);
        }
        [HttpPost]
        public IActionResult BlueTick([FromForm] string description)
        {
            if(description == null)
            {
                return Redirect("/home/error");
            }
            var blueTickApplication=new BlueTickApplication()
            {
                UserId=_userManager.GetUserId(User),
                Description=description,
                IsApplication=true
            };
            _blueTickApplicationServices.Create(blueTickApplication);
            TempData["Message"] = "Başvurunuz alındı.";
            TempData["Alert"] = "alert-success";
            return Redirect("/user/settings");
        }
        public IActionResult AddStory()
        {
            var storyModel = new StoryModel();
            return View(storyModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddStory([FromForm] IFormFile file, [FromForm] string Description)
        {
            var user=await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var story = new Story(){
                UserId=user.Id,
                UserName=user.UserName,
                Description=Description,
                CreatedAt=DateTime.Now
            };
            if(file!=null&&file.Length!=0)
            {
                var fileName=Guid.NewGuid()+".jpeg";
                var path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","storyImages",fileName);
                using(var stream=new FileStream(path,FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                story.ImageUrl=fileName;

                _storyServices.Create(story);
            }
            return Redirect("/");
        }
        [HttpPost]
        public IActionResult Complaint([FromForm] int postId)
        {
            TempData["PostId"]=postId.ToString();
            if(!_complaintServices.DidYouComplaint(_userManager.GetUserId(User),postId))
            {
                var complaint = new Complaint(){
                    PostId=postId,
                    UserId=_userManager.GetUserId(User),
                    Content="Şikayet",
                    CreatedAt=DateTime.Now
                };
                _complaintServices.Create(complaint);
                
                TempData["MessageComplaint"]="Şikayet talebiniz alınmıştır! PostPal şikayetinizi inceleyecektir. Daha iyi bir sosyal medya deneyimi için çalışmaktayız.";
                return Redirect("/home/index/#"+postId.ToString());
            }
            TempData["MessageComplaint"]="Bu postu zaten şikayet ettiniz şuan inceleniyor!";
            return Redirect("/home/index/#"+postId.ToString());
        }

        
    }
    
}