using RestaurantDao.Models;

namespace EndUserPortal.Models.ViewModels
{
    public class IndexViewModel
    {
        public List<Restaurant> Restaurants { get; set; }
        public List<RestCategory> RestCategorys { get; set; }
        public List<Restaurant> RestWeeklyTrends { get; set; }

        public string? Message { get; set; }
    }
}
