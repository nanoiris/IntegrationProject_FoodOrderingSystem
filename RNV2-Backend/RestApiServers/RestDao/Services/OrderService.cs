using RestaurantDaoBase.Enums;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public class OrderService : IOrderService
    {
        public Task<bool> AddDishToCart(string orderId, string menuId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelOrder(string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCart(string email, string menuId, string restaurantId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DecreaseDishQty(string email, string orderItemId, string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> FindOrderById(string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncreaseDishQty(string email, string orderItemId, string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>?> ListActiveOrder()
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>?> ListOrderByStatus(string restaurantId, StatusEnum status)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, Order>> ListOrderByUser(string email)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>?> ListOrderByUserAndStatus(string email, StatusEnum status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PayCart(string email, string orderId, PayCard card)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>?> SearchOrder(string restaurantId, string email, StatusEnum status)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateCart(string email, string menuId, string restaurantId, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderStatus(string restaurantId, string orderId, StatusEnum newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
