using Microsoft.AspNetCore.Mvc;
using RMS.Models;
using System.Diagnostics;
using RestaurantDao.IServices;
using RestaurantDao.Services;

namespace RMS.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService service;


        public AuthController(ILogger<HomeController> logger, IAuthService service)
        {
            _logger = logger;
            this.service = service;
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

        public IActionResult Logout(string username)

        {
            service.Logout();
            HttpContext.Session.Clear();
            // service.Logout(@User.Identity.Name);
            TempData["SuccessMessage"] = "Successfully logged out.";

            return RedirectToAction("Login", "Auth");

        }

    }
}