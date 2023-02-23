using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantDaoBase.Models;
using MenuItem = RestaurantDaoBase.Models.MenuItem;

namespace RestaurantNetowrkApp.Services.IServices
{
    interface IRestaurantService
    {
        Task< List<RestCategory>> ListCategory();
        Task<List<Restaurant>> ListWeeklyTrends();
        Task<List<Restaurant>> ListWithLimit(int limit);
        Task<List<Restaurant>> Search(string searchKey, string categoryName);
        Task<Restaurant> GetRestById(string id);
        Task<List<MenuItem>> GetFeaturedMenus(string restaurantId);
  
    }
}
