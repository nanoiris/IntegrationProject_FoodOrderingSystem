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
        public List<Restaurant> ListWeeklyTrends();
        public List<Restaurant> ListWithLimit(int limit);
        public List<Restaurant> Search(string searchKey, string categoryName, string sortField);

        public void AddRestaurant(Restaurant restaurant, Stream logo);
        public List<Restaurant> ListRestaurant();
        public Restaurant? FindRestaurant(string restaurantId);
        public void DeleteRestaurant(string restaurantId);
        public void UpdateRestaurant(Restaurant restaurant, Stream logo);

        public Task<RestCategory?> FindCategory(string categoryId);
        public Task<bool> DeleteCategory(string categoryId);
        public Task<bool> AddCategory(RestCategory category);
        public Task<bool> UpdateCategory(RestCategory categor);
    }
}
