using Microsoft.AspNetCore.Mvc;
using RMS.Models;
using System.Diagnostics;
using RestaurantDao.IServices;

namespace RMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService service;

        public HomeController(ILogger<HomeController> logger, IAuthService service)
        {
            _logger = logger;
            this.service = service;
        }

        public IActionResult Index()
        {
            var avatar = HttpContext.Session.GetString("a");
            _logger.LogInformation("a************ " + avatar);
            var restaurantId = HttpContext.Session.GetString("RestaurantId");
            _logger.LogInformation("r************ " + restaurantId);

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appUser = service.Login(model.Email, model.Password);
                if (appUser != null)
                {
                    HttpContext.Session.SetString("a", "avatar");
                    HttpContext.Session.SetString("RestaurantId", appUser.Restaurant.Id.ToString());

                    return RedirectToAction("Index", "Order");
                }
            }
            TempData["FailureMessage"] = "Invalid login attempt.";
            return View();

        }
    }
}