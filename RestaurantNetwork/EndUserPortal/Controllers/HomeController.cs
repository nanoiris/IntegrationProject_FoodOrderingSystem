using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EndUserPortal.Models;
using RestaurantDao.Models;
using EndUserPortal.Models.ViewModels;
using Microsoft.Identity.Client;
using RestaurantDao.IServices;
using Microsoft.AspNetCore.Authorization;

namespace EndUserPortal.Controllers;

public class HomeController : Controller
{
    private IRestarantService restaurantService;
    private readonly ILogger<HomeController> logger;

    public HomeController(ILogger<HomeController> logger, IRestarantService restaurantService)
    {
        this.logger = logger;
        this.restaurantService= restaurantService;
    }

    public IActionResult Index(string? message)
    {
        List<RestCategory> restCategorys = restaurantService.ListCategory();
        List<Restaurant> restaurants = restaurantService.ListWithLimit(4);
        List<Restaurant> restWeeklyTrends = restaurantService.ListWeeklyTrends();
        IndexViewModel indexViewModel = new IndexViewModel()
        {
            Restaurants = restaurants,
            RestCategorys = restCategorys,
            RestWeeklyTrends = restWeeklyTrends
        };
        if (message != null)
        {
            indexViewModel.Message = message;
        }
        return View(indexViewModel);
    }

    [HttpGet]
    [HttpPost]
    public IActionResult Restaurants(RestaurantsViewModel model)
    {
        
        List<RestCategory> restCategories = restaurantService.ListCategory();
        
        model.Restaurants = restaurantService.Search(model.SearchKey, model.CategoryKey, null);
        model.RestCategories = restCategories;
        
        return View(model);
    }


    [HttpGet]
    public IActionResult SearchByCategory(int id,string name)
    {
        RestaurantsViewModel model = new RestaurantsViewModel();
        model.RestCategories = restaurantService.ListCategory();

        model.CategoryKey = name;
        model.Restaurants = restaurantService.Search(null,model.CategoryKey, null);
        return View("Restaurants",model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
