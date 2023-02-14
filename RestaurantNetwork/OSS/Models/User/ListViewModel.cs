using RestaurantDao.Models;

namespace OSS.Models.User
{
    public class ListViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[] 
        {
           new RoutePath("Home", "Index", "Home"),
           new RoutePath("User", "Index","User")
        };
        public string[] TabHeaders { get; set; } = new string[] { "Avatar", "Name", "Email", "Phone", "Delete" };
        public int RestaurantId { get; set; }
        public List<RestaurantDao.Models.AppUser>? Rows { get; set; }

    }
}
