using RestaurantDao.Models;

namespace EndUserPortal.Models.ViewModels
{
    public class MyOrderViewModel
    {
        public List<Order> ordersCart { get; set; }
        public List<Order> ordersReady { get; set; }
        public List<Order> ordersOnProgress { get; set; }
        public List<Order> ordersCompleted { get; set; }
        public List<Order> ordersCancled { get; set; }

        public string? cancleOrderSuccess { get; set; }
        public DateTime PickUpTime { get; set; }
    }
}
