using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.IServices
{
    public interface IRestaurantService
    {
        public Task<List<RestCategory>> ListCategory();
        //public Task<WeeklyTrend> ListWeeklyTrends();
        public Task<List<Restaurant>> ListWeeklyTrends();
        public Task<List<Restaurant>> ListWithLimit(int limit);
        public Task<List<Restaurant>> Search(string searchKey, string categoryName);

        public Task<string> AddRestaurant(RestaurantForm form);
        public Task<List<Restaurant>> ListRestaurant();
        public Task<Restaurant?> FindRestaurant(string restaurantId);
        public Task<bool> DeleteRestaurant(string restaurantId);
        public Task<bool> UpdateRestaurant(RestaurantForm form);
        public Task<bool> UpdateRestaurantLogo(Restaurant model);

        public Task<RestCategory?> FindCategory(string categoryId);
        public Task<bool> DeleteCategory(string categoryId);
        public Task<AppResult> AddCategory(RestCategory category);
        public Task<bool> UpdateCategory(RestCategory categor);
        
    }
}
