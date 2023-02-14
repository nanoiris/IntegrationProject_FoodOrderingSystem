using Microsoft.AspNetCore.Identity;
using RestaurantDao.Models;

namespace OSS.Models.Order
{
    public class ListViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[]
        {
            new RoutePath("Home", "Index", "Home"),
            new RoutePath("Order", "Index","Order")
        };
        public string[] TabHeaders { get; set; } = new string[] { "OrderNo.", "Restaurant", "Customer", "Total", "Status", "Desire Time", "Payment Type", "Detail", "Delete" };
        
        public List<RestaurantDao.Models.Order>? Rows { get; set; }
        public string? SearchKey { get; set; }

    }
}
