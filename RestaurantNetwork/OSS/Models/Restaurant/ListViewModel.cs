using RestaurantDao.Models;

namespace OSS.Models.Restaurant
{
    public class ListViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[]
        {
          new RoutePath("Home", "Index", "Home"),
          new RoutePath("Restaurant", "Index","Restaurant")
        };
        public string[] TabHeaders { get; set; } = new string[] { "ID", "Picture", "Featured", "Name", "Address", "City", "Phone", "Category", "Edit", "Delete", "Add User" };
        public List<RestaurantDao.Models.Restaurant>? Restaurants { get; set; }

    }
}
