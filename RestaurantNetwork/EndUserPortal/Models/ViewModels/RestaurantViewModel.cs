using Microsoft.AspNetCore.Mvc;
using RestaurantDao.Models;

namespace EndUserPortal.Models.ViewModels
{
    public class RestaurantViewModel
    {
        public Restaurant Restaurant { get; set; }
        public Order Order { get; set; }
        public List<MenuItem> FeaturedMenus { get; set; }

        public int MenuItemID { get; set; }

        public double payTotal { get; set; }

        public string restaurantName { get; set; }

        public DateTime pickupTime { get; set; }

        public int orderId { get; set; }
        public string[] Ratings { get; set; }
        public int TotalRatings { get; set; }
    }
}
