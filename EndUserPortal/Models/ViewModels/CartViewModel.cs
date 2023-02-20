using RestaurantDao.Models;

namespace EndUserPortal.Models.ViewModels
{
    public class CartViewModel
    {
        public Restaurant Restaurant { get; set; }
        public Order Order { get; set; }

        public double payTotal { get; set; }

        public string restaurantName { get; set; }

        public DateTime pickupTime { get; set; }

        public int orderId { get; set; }
    }
}
