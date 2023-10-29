using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SocialMedia.business.Abstract;
using SocialMedia.data.Abstract;
using SocialMedia.webapp.Identity;
using SocialMedia.webapp.Models;

namespace SocialMedia.webapp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IBlueTickApplicationServices _blueTickApplicationServices;
        private readonly IPostServices _postServices;
        private readonly ILikeServices _likeServices;
        private readonly ICommentServices _commentServices;
        private readonly IFollowerServices _followerServices;
        private readonly IFollowUpServices _followUpServices;
        private readonly ISaveServices _saveService;
        private readonly INotificationServices _notificationServices;
        private readonly IComplaintServices _complaintServices;
        private readonly IFeedbackServices _feedbackServices;
        private readonly UserManager<User> _userManager;
        public AdminController(IBlueTickApplicationServices blueTickApplicationServices, IPostServices postServices, UserManager<User> userManager, ILikeServices likeServices, ICommentServices commentServices, IFollowerServices followerServices, IFollowUpServices followUpServices, ISaveServices saveService, INotificationServices notificationServices, IComplaintServices complaintServices, IFeedbackServices feedbackServices)
        {
            _blueTickApplicationServices=blueTickApplicationServices;
            _postServices=postServices;
            _userManager=userManager;
            _likeServices=likeServices;
            _commentServices=commentServices;
            _followerServices=followerServices;
            _followUpServices=followUpServices;
            _saveService=saveService;
            _notificationServices=notificationServices;
            _complaintServices=complaintServices;
            _feedbackServices=feedbackServices;
        }
        public async Task<IActionResult> Dashboard()
        {
            var user=await _userManager.FindByNameAsync(_userManager.GetUserName(User));
            var dashboardModel = new DashboardModel(){
                NumberOfUsers=_userManager.Users.Count(),
                NumberOfBlueTickApplications=_blueTickApplicationServices.GetBlueTickApplications().Count(),
                UserName=user.UserName,
                ImageUrl=user.ImageUrl,
                Roles=await _userManager.GetRolesAsync(user),
                NumberOfComplaints=_complaintServices.TotalComplaintsCount(),
                NumberOfFeedbacks=_feedbackServices.TotalFeedbackCount()
            };
            return View(dashboardModel);

        }

        public async Task<IActionResult> UserList()
        {
            var users = _userManager.Users.OrderByDescending(i=>i.Id).Take(50);
            var userModels=new List<UserModel>(){};
            foreach(var user in users)
            {
                if(!await _userManager.IsInRoleAsync(user,"Admin"))
                {
                    userModels.Add(new UserModel(){
                        Id=user.Id,
                        UserName=user.UserName,
                        ImageUrl=user.ImageUrl,
                        IsApproved=user.IsApproved,
                        IsActive=user.IsActive
                    });
                }   
            }
            return View(userModels);
        }
        [HttpPost]
        public async Task<IActionResult> UserList([FromForm] string search)
        {   
            var userModels=new List<UserModel>(){};
            if(search==null)
            {
                var users = _userManager.Users.OrderByDescending(i=>i.Id).Take(50);
                foreach(var user in users)
                {
                    if(!await _userManager.IsInRoleAsync(user,"Admin"))
                    {
                        userModels.Add(new UserModel(){
                            Id=user.Id,
                            UserName=user.UserName,
                            ImageUrl=user.ImageUrl,
                            IsApproved=user.IsApproved,
                            IsActive=user.IsActive
                        });
                    }   
                }
            }
            else
            {
                var user2 = await _userManager.FindByNameAsync(search);
                if(user2 == null)
                {
                    TempData["Message"]=search+" kullanıcı bulunamadı!";
                    TempData["Alert"]="alert-danger";
                    var users = _userManager.Users.OrderByDescending(i=>i.Id).Take(50);
                    foreach(var user in users)
                    {
                        if(!await _userManager.IsInRoleAsync(user,"Admin"))
                        {
                            userModels.Add(new UserModel(){
                                Id=user.Id,
                                UserName=user.UserName,
                                ImageUrl=user.ImageUrl,
                                IsApproved=user.IsApproved,
                                IsActive=user.IsActive
                            });
                        }   
                    }
                    return View(userModels);
                }
                var userModel= new UserModel(){
                    Id=user2.Id,
                    UserName=user2.UserName,
                    IsApproved=user2.IsApproved,
                    ImageUrl=user2.ImageUrl,
                    IsActive=user2.IsActive
                };
                userModels.Add(userModel);
            }
            
            
            
            return View(userModels);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteApproved([FromForm] string userName)
        {
            if(userName == null)
            {
                return Redirect("/home/error");
            }
            var user = await _userManager.FindByNameAsync(userName);
            if(user==null)
            {
                return Redirect("/home/error");
            }
            user.IsApproved=false;
            await _userManager.UpdateAsync(user);
            return Redirect("/admin/userlist");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromForm] string userName)
        {
            if(userName == null)
            {
                return Redirect("/home/error");
            }
            var user = await _userManager.FindByNameAsync(userName);
            if(user==null)
            {
                return Redirect("/home/error");
            }
            if(user.IsActive==true)
            {
                user.IsActive=false;
                await _userManager.UpdateAsync(user);
                await _userManager.RemoveFromRoleAsync(user,"User");
                await _userManager.AddToRoleAsync(user,"Block");
            }
            else
            {
                user.IsActive=true;
                await _userManager.UpdateAsync(user);
                await _userManager.RemoveFromRoleAsync(user,"Block");
                await _userManager.AddToRoleAsync(user,"User");
            }
            
            
            return Redirect("/admin/userlist");
        }
        public async Task<IActionResult> ApplicationList()
        {
            var Applications=_blueTickApplicationServices.GetBlueTickApplications();

            var applicationModels = new List<BlueTickApplicationModel>(){};
            foreach(var item in Applications)
            {
                var user = await _userManager.FindByIdAsync(item.UserId);
                if(user != null)
                {
                    applicationModels.Add(new BlueTickApplicationModel(){
                        Id=item.Id,
                        UserId=item.UserId,
                        UserName=user.UserName,
                        Description=item.Description,
                        IsApplication=item.IsApplication
                    });
                }
                
            }
            return View(applicationModels);
        }
        [HttpPost]
        public async Task<IActionResult> ApplicationList([FromForm] string search)
        {
            var applicationModels = new List<BlueTickApplicationModel>(){};
            

            if(search!=null)
            {
                var user = await _userManager.FindByNameAsync(search);
                
                if(user != null)
                {
                    var temp= _blueTickApplicationServices.GetBlueTickApplicationByUserId(user.Id);
                    applicationModels.Add(new BlueTickApplicationModel(){
                        Id=temp.Id,
                        UserId=temp.UserId,
                        UserName=user.UserName,
                        Description=temp.Description,
                        IsApplication=temp.IsApplication
                    });
                }
                else
                {
                    TempData["Message"]=search+" kullanıcı bulunamadı!";
                    TempData["Alert"]="alert-danger";
                }
            }
            else
            {
                var Applications=_blueTickApplicationServices.GetBlueTickApplications();
                foreach(var item in Applications)
                {
                    var user = await _userManager.FindByIdAsync(item.UserId);
                    if(user != null)
                    {
                        applicationModels.Add(new BlueTickApplicationModel(){
                            Id=item.Id,
                            UserId=item.UserId,
                            UserName=user.UserName,
                            Description=item.Description,
                            IsApplication=item.IsApplication
                        });
                    }
                }
            }
            
            return View(applicationModels);
        }
        [HttpPost]
        public async Task<IActionResult> BlueTickActive([FromForm] string userName, [FromForm] int id)
        {
            if(userName==null)
            {
                return Redirect("/home/error");
            }
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return Redirect("/home/error");
            }
            user.IsApproved=true;
            await _userManager.UpdateAsync(user);
            var blueTickApplication=_blueTickApplicationServices.GetBlueTickApplicationById(id);
            _blueTickApplicationServices.Delete(blueTickApplication);
            return Redirect("/admin/applicationlist");
        }
        public IActionResult ComplaintList()
        {
            var complaints=_complaintServices.GetComplaint();
            var complaintModels = new List<ComplaintModel>(){};
            foreach(var complaint in complaints)
            {
                var post=_postServices.GetPostById(complaint.PostId);
                complaintModels.Add(new ComplaintModel(){
                    Id=complaint.Id,
                    PostId=complaint.PostId,
                    ImageUrl=post.ImageUrl,
                    Content=complaint.Content,
                    NumberOfUser=_complaintServices.TotalComplaintsCountByPost(complaint.PostId)
                });
            }
            complaintModels=complaintModels.OrderByDescending(i=>i.NumberOfUser).ToList();
            return View(complaintModels);
        }
        [HttpPost]
        public IActionResult Search([FromForm] int search)
        {
            var complaintModels = new List<ComplaintModel>(){};
            var complaint = _complaintServices.GetComplaintsByPostId(search).FirstOrDefault();
            if(complaint == null)
            {
                TempData["Message"]=search+" idli postun şikayeti bulunamadı!";
                TempData["Alert"]="alert-danger";
                return View("ComplaintList",complaintModels);
            }
            var post=_postServices.GetPostById(complaint.PostId);
            complaintModels.Add(new ComplaintModel(){
                Id=complaint.Id,
                PostId=complaint.PostId,
                ImageUrl=post.ImageUrl,
                Content=complaint.Content,
                NumberOfUser=_complaintServices.TotalComplaintsCountByPost(complaint.PostId)
            });
            
            return View("ComplaintList",complaintModels);
        }
        [HttpPost]
        public IActionResult RemovePost(int postId)
        {
            var post = _postServices.GetPostById(postId);
            if(post!=null)
            {
                var likes=_likeServices.GetLikesByPostId(postId);
                var comments=_commentServices.GetCommentsByPostId(postId);
                var saves=_saveService.GetSavesByPostId(postId);
                var notifications=_notificationServices.GetNotificationsByPost(postId);
                var complaints=_complaintServices.GetComplaintsByPostId(postId);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","postImages",post.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _likeServices.DeleteMultiple(likes);
                _commentServices.DeleteMultiple(comments);
                _saveService.DeleteMultiple(saves);
                _notificationServices.DeleteMultiple(notifications);
                _postServices.Delete(post);
                _complaintServices.DeleteMultiple(complaints);
                
            }
            return Redirect("/Admin/ComplaintList");
        }
        [HttpPost]
        public ActionResult Ignore([FromForm] int postId)
        {
            var complaints=_complaintServices.GetComplaintsByPostId(postId);
            _complaintServices.DeleteMultiple(complaints);
            return Redirect("/Admin/ComplaintList");
        }
        public IActionResult FeedbackList()
        {
            var feedbacks=_feedbackServices.GetFeedback();
            var feedbackModels = new List<FeedbackModel>(){};
            foreach(var feedback in feedbacks)
            {
                feedbackModels.Add(new FeedbackModel(){
                    Id = feedback.Id,
                    UserId=feedback.UserId,
                    ImageUrl=feedback.ImageUrl,
                    CreatedAt=feedback.CreatedAt,
                    Content=feedback.Content
                });
            }
            return View(feedbackModels);
        }
        [HttpPost]
        public IActionResult Complated([FromForm] int id)
        {
            var feedback = _feedbackServices.GetFeedbackById(id);
            if(feedback!=null)
            {
                _feedbackServices.Delete(feedback);
                TempData["Message"]="Geri bildirim çözüldü!";
                TempData["Alert"]="alert-success";
            }
            return Redirect("/admin/feedbacklist");
        }
    }

}