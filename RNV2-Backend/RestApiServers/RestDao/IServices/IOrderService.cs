using RestaurantDao.Enums;
using RestaurantDao.Models;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.IServices
{
    public interface IOrderService
    {
        public Order FindOrderById(string orderId);
        public void CancelOrder(string orderId);
        public List<Order> ListActiveOrder();

        public List<Order> ListOrderByUserAndStatus(string email,StatusEnum status);
        public Dictionary<string,Order> ListOrderByUser(string email);
        
        public void AddDishToCart(string orderId,string menuId);
        public void CreateCart(string email, string menuId, string restaurantId);
        public string UpdateCart(string email, string menuId, string restaurantId,int orderId);

        public void IncreaseDishQty(string email, string orderItemId,string orderId);
        public void DecreaseDishQty(string email, string orderItemId,string orderId);

        public void PayCart(string email,string orderId,PayCard card);

        public void UpdateOrderStatus(string restaurantId, string orderId, StatusEnum newStatus);

        public List<Order> ListOrderByStatus(string restaurantId, StatusEnum status);
        public List<Order> SearchOrder(string restaurantId, string email, StatusEnum status);

    }
}
