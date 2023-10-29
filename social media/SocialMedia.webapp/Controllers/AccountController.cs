using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SocialMedia.business.Abstract;
using SocialMedia.webapp.Identity;
using SocialMedia.webapp.Models;

namespace SocialMedia.webapp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailServices _emailService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailServices emailService)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _emailService=emailService;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginModel.UserName);
                if (user == null)
                {
                    TempData["Message"] ="Girilen bilgilere göre kullanıcı kayıtlı değil!";
                    TempData["Alert"] ="alert-danger";
                    return View();
                }
                var result = await _signInManager.PasswordSignInAsync(user,loginModel.Password,true,false);
                if(result.Succeeded)
                {
                    if(await _userManager.IsInRoleAsync(user,"Admin"))
                    {
                        return Redirect("/Admin/Dashboard");
                    }
                    else
                    {   
                        if(user.IsActive)
                        {
                            TempData["Message"] ="Giriş Başarılı.";
                            TempData["Alert"] ="alert-success";
                            return Redirect("/");
                        }
                        else
                        {
                            await _signInManager.SignOutAsync();
                            TempData["Message"] ="Hesabın banlandı!";
                            TempData["Alert"] ="alert-danger";
                            return View();
                        }
                        
                    }
                    
                }
                else
                {
                    TempData["Message"] ="Girilen bilgilere göre şifre yanlış!";
                    TempData["Alert"] ="alert-danger";
                    return View();
                }
            }
            return View(loginModel);
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel, [FromForm] IFormFile file)
        {
            if(ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email,
                    IsActive=true
                };

                if(file!=null && file.Length!=0)
                {
                    var fileName = user.UserName+".jpeg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","userImages",fileName);
                    using (var stream = new FileStream(path,FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    user.ImageUrl=fileName;
                }
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if(result.Succeeded)
                {
                    var user2 = await _userManager.FindByNameAsync(user.UserName);
                    await _userManager.AddToRoleAsync(user2,"User");
                    TempData["Message"] = "Kaydınız başarıyla oluşturuldu!";
                    TempData["Alert"] = "alert-success";
                    return Redirect("/account/login");
                }
            }
            return View(registerModel);
        }
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromForm] string Email)
        {
            if(Email==null)
            {
                return Redirect("/home/error");
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if(user!=null)
            {
                Random rnd = new Random();
                string newPassword = "Gopost"+rnd.Next(100000,999999);
                _emailService.SendEmail(Email,newPassword);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result =  await _userManager.ResetPasswordAsync(user, token, newPassword);
                if(result.Succeeded)
                {
                    _emailService.SendEmail(Email,newPassword);
                    TempData["Message"] = "Yeni Şifreniz mail adresinize gönderildi!";
                    TempData["Alert"] = "alert-success";
                }
                else
                {
                    TempData["Message"] = "Gönderme başarısız!";
                    TempData["Alert"] = "alert-danger";
                }
            }
            return View();
        }
    }
}