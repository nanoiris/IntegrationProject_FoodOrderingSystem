using RestaurantDaoBase.Enums;

namespace OssApp.Model
{
    public class OrderItem
    {
        public MenuItem? Item { get; set; }
        public int? Qty { get; set; }
    }
}
