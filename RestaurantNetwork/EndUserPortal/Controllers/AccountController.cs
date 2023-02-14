using EndUserPortal.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantDao;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using Stripe;
using System.IO;

namespace EndUserPortal.Controllers
{

    public class AccountController : Controller
    {

        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IAuthService authService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, IAuthService authService)
        {
            _logger = logger;
            this.authService = authService;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            SignupViewModel model = new SignupViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var idUser = new IdentityUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
                AppUser newUser = new AppUser
                {
                    Name = model.Name,
                    PhoneNo = model.PhoneNo,
                    Email = model.Email,
                    IdUser = idUser,
                    Password = model.Password,
                    Role = "Consumer",
                };
                Stream stream = null;
                if (model.Upload != null)
                {
                    newUser.Logo = model.Upload.FileName;
                    stream = model.Upload.OpenReadStream();
                }
                authService.AddUser(newUser, stream);

                return RedirectToAction("Login", "Account", new {message = "Sign Up Successfully, Please Login." });

            }
            model.Message = "Failed to add the user";
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string? message)
        {
            LoginViewModel model = new LoginViewModel();
            model.Message = message;
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appUser = authService.Login(model.Email, model.Password);
                if (appUser != null)
                {
                    HttpContext.Session.SetInt32("AppUserId", appUser.Id);
                    HttpContext.Session.SetString("Avatar", appUser.Avatar);
                    HttpContext.Session.SetString("UserName", appUser.Name);
                    HttpContext.Session.SetString("UserEmail", appUser.Email);
                    return RedirectToAction("Index", "Home", new { message = "Login Successfully! Welcome " + appUser.Name });
                }
                else
                {
                    model.Message = "Login failed, try again.";
                    return View(model);
                }
            }

            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Profile(string? message)
        {
            int appUserId = (int)HttpContext.Session?.GetInt32("AppUserId");
            AppUser appUser = authService.FindAppUserById(appUserId);
            ProfileViewModel model = new ProfileViewModel();
            model.Avatar = appUser.Avatar;
            model.Name = appUser.Name;
            model.Email = appUser.Email;
            model.Phone = appUser.PhoneNo;
            model.Id = appUserId;
            model.idUserId = appUser.IdUser.Id;
            if (message != null)
            {
                model.Message = message;
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNo = model.Phone,
                };
                IdentityUser idUser = new IdentityUser
                {
                    Id = model.idUserId
                };
                user.IdUser = idUser;

                Stream stream = null;
                if (model.UploadLogo != null)
                {
                    user.Logo = model.UploadLogo.FileName;
                    stream = model.UploadLogo.OpenReadStream();
                }
                authService.UpdateUser(user, stream);
                HttpContext.Session.SetString("Avatar", user.Avatar);
                HttpContext.Session.SetString("UserName", user.Name);

                return RedirectToAction("Profile", "Account", new { message = "Edit profile Successfully!" });
            }
            model.Message = "Update failed, try again.";
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            authService.Logout();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
