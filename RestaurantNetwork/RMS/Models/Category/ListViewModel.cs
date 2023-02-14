using RestaurantDao.Models;

namespace RMS.Models.Category
{
    public class ListViewModel
    {
        // public string? Message { get; set; }
        public RoutePath[] Paths { get; set; }
        public string[] TabHeaders { get; set; }
        public ListViewModel()
        {
            Paths = new RoutePath[]
            {
               new RoutePath { ControllerName = "Home", ActionName = "Index", Titile = "Home" },
               new RoutePath { ControllerName = "Category", ActionName = "Index",Titile = "Category"}
            };
            TabHeaders = new string[] { "ID", "Name", "Edit", "Delete" };
        }
        public List<MenuCategory>? Categories { get; set; }

    }
}
