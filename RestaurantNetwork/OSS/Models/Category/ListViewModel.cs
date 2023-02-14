using Microsoft.AspNetCore.Identity;
using RestaurantDao.Models;

namespace OSS.Models.Category
{
    public class ListViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[]
        {
            new RoutePath("Home", "Index", "Home"),
            new RoutePath("Category","Index","Category")
        };
        public string[] TabHeaders { get; set; } = new string[] { "", "ID", "Name", "Logo", "Delete" };
        public List<RestCategory>? Rows { get; set; }
    }
}
