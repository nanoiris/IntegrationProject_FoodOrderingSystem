using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.IServices
{
    public interface IRestarantService
    {
        List<RestCategory> ListCategory();
        List<Restaurant> ListWeeklyTrends();
        List<Restaurant> ListWithLimit(int limit);
        public List<Restaurant> Search(string searchKey, string categoryName, string sortField);

        public void AddRestaurant(Restaurant restaurant, Stream logo);
        public List<Restaurant> ListRestaurant();
        public Restaurant? FindRestaurant(string restaurantId);
        public void DeleteRestaurant(string restaurantId);
        public void UpdateRestaurant(Restaurant restaurant, Stream logo);

        public RestCategory? FindCategory(string categoryId);
        public void DeleteCategory(string categoryId);
        public void AddCategory(RestCategory category, Stream logo);
        public void UpdateCategory(RestCategory category, Stream logo);
    }
}
