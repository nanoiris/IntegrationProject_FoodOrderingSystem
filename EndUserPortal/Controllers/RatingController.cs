using EndUserPortal.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantDao;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RestaurantDao.Services;

namespace EndUserPortal.Controllers
{

    [Authorize]
    public class RatingController : Controller
    {
        private IRestarantService restaurantService;
        private IRatingService ratingService;
        private IOrderService orderService;
        private readonly ILogger<RatingController> _logger;
        public RatingController(ILogger<RatingController> logger, IRestarantService restaurantService, IRatingService ratingService, IOrderService orderService)
        {
            _logger = logger;
            this.restaurantService = restaurantService;
            this.ratingService = ratingService;
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult Rate(int value, int restId)
        {
            Rating newRating = new Rating();
            newRating.Value= value;
            newRating.CreateTime= DateTime.Now;
            //newRating.CreateBy = HttpContext.Session.GetString("UserName");

            newRating.CreateBy = HttpContext.Session.GetString("UserEmail");
            ratingService.AddRating(newRating, restId);

            //return RedirectToAction("Restaurant","Order", new { id = restId });
            return buildRatingPartialView(restId);
        }

        public IActionResult buildRatingPartialView(int id)
        {
            Restaurant restaurant = restaurantService.GetRestById(id);
            String[] ratings = restaurantService.CalculateRatings(id);
            int totalRatings = restaurantService.CalculateTotalRatings(id);

            RestaurantViewModel model = new RestaurantViewModel
            {
                Restaurant = restaurant,
                Ratings = ratings,
                TotalRatings= totalRatings

            };
            return PartialView("../Order/Rating", model);
        }

        //[HttpGet]
        //public IActionResult AddRate()
        //{
        //    Rating newRating = new Rating()
        //    {
        //        Value = 1,
        //        CreateTime = DateTime.Now,
        //        CreateBy = "panera@rn.com"
        //    };

        //    using (var db = new AppDbContext())
        //    {
        //        newRating.Target = db.Restaurants.FirstOrDefault(x => x.Id == 6);
        //        for(int i =0; i< 6; i++)
        //        {

        //            db.Ratings.Add(newRating);
        //            db.SaveChangesAsync().GetAwaiter().GetResult();
        //        }

        //    }
        //    return RedirectToAction("Index", "Home");
        //}

    }
}
