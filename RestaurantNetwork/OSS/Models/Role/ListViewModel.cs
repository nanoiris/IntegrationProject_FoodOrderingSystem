using Microsoft.AspNetCore.Identity;
using RestaurantDao.Models;

namespace OSS.Models.Role
{
    public class ListViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[] 
        {
            new RoutePath("Home", "Index", "Home"),
            new RoutePath("Role", "Index","Role")
        };
        public string[] TabHeaders { get; set; } = new string[] { "ID", "Name", "Delete" };
        public List<IdentityRole>? Rows { get; set; }

    }
}
