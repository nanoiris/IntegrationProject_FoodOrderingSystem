﻿using RestaurantDaoBase.Enums;
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
        public Task<List<Order>?> ListActiveOrder();

        public Task<List<Order>?> ListOrderByUserAndStatus(string email,StatusEnum status);
        public Task<Dictionary<string,Order>> ListOrderByUser(string email);
        
        public Task<bool> AddDishToCart(string orderId,string menuId);
        public Task<bool> CreateCart(string email, string menuId, string restaurantId);
        public Task<string> UpdateCart(string email, string menuId, string restaurantId,int orderId);

        public Task<bool> IncreaseDishQty(string email, string orderItemId,string orderId);
        public Task<bool> DecreaseDishQty(string email, string orderItemId,string orderId);

        public Task<bool> PayCart(string email,string orderId,PayCard card);

        public Task<bool> UpdateOrderStatus(string restaurantId, string orderId, StatusEnum newStatus);

        public Task<List<Order>?> ListOrderByStatus(string restaurantId, StatusEnum status);
        public Task<List<Order>?> SearchOrder(string restaurantId, string email, StatusEnum status);

    }
}
