using RestaurantDao.Models;

namespace EndUserPortal.Models.ViewModels
{
    public class RestaurantsViewModel
    {
        public List<RestCategory> RestCategories { get; set; }
        public List<Restaurant> Restaurants { get; set;}

        public string? SearchKey { get; set; }
       // public string? SortKey { get; set; }
        public string? CategoryKey { get; set; }
    }
}
