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
        public Restaurant GetRestById(int id);
        public List<MenuItem> GetFeaturedMenus(int restaurantId);
        public string[] CalculateRatings(int restaurantId);

        public int CalculateTotalRatings(int restaurantId);
    }
}
