using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSS.Models;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using System.Diagnostics;

namespace OSS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IAuthService authService;

        public HomeController(ILogger<HomeController> logger,IAuthService authService)
        {
            this.logger = logger;
            this.authService = authService;

        }
        [Authorize(Roles = "Operator")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                AppUser user = authService.Login(model.Email,model.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("Avatar", user.Avatar);
                    HttpContext.Session.SetString("UserName", user.Name);
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserRole", user.Role);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Logout()
        {
            authService.Logout();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}