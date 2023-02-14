using RestaurantDao.Models;

namespace RMS.Models.Item
{
    public class ListViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; }
        public string[] TabHeaders { get; set; }
        public ListViewModel()
        {
            Paths = new RoutePath[]
            {
               new RoutePath { ControllerName = "Home", ActionName = "Index", Titile = "Home" },
               new RoutePath { ControllerName = "Item", ActionName = "Index",Titile = "Food"}
            };
            TabHeaders = new string[] { "ID", "Picture", "Name", "Category", "Price($)", "Discount($)", "Edit", "Delete" };
        }
        public List<MenuItem>? Items { get; set; }

        public List<MenuCategory>? Categories { get; set; }

    }
}
