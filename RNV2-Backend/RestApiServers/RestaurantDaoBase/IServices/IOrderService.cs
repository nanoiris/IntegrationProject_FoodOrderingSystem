using RestaurantDaoBase.Enums;
using RestaurantDaoBase.Models;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.IServices
{
    public interface IOrderService
    {
        public Task<Order> FindOrderById(string orderId);
        public Task<bool> CancelOrder(string orderId);
        public Task<List<Order>> ListActiveOrder();

        public Task<List<Order>> ListOrderByUserAndStatus(string email,StatusEnum status);
        public Task<Dictionary<string, List<Order>>> ListOrderByUser(string email);
        
        public Task<bool> AddDishToCart(string orderId, MenuItem menuItem);
        public Task<bool> CreateCart(string email, MenuItem menuItem, string restaurantId, string restaurantName);
        
        public Task<bool> IncreaseDishQty(string orderItemId,string orderId);
        public Task<bool> DecreaseDishQty(string orderItemId,string orderId);

        public Task<bool> PayCart(string orderId,PayCard card);

        public Task<bool> UpdateOrderStatus(string orderId, StatusEnum newStatus);
        public Task<bool> ChangeDelivery(string orderId, bool isDelivery);
        public Task<List<Order>> ListOrderByStatus(string restaurantId, StatusEnum status);
        public Task<List<Order>> SearchOrder(string restaurantId, string email, StatusEnum status);
        public Task<List<Order>> SearchOrder(string restaurantId, string email);

    }
}
