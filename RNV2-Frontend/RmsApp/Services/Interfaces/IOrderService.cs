using System.Collections.Generic;
using System.Threading.Tasks;
using RmsApp.Dtos;
using RestaurantDaoBase.Enums;

namespace RmsApp.Services
{
    public interface IOrderService
    {
        Task<List<OrderListDto>> ListOrderAsync(string restaurantId, int status);
        Task<bool> UpdateOrderStatusAsync(string orderId, int status);
        Task<OrderListDto> GetOrderAsync(string orderId);
        // Task<OrderDetailDto> FindOrderByIdAsync(int restaurantId, int orderId);
        // Task UpdateOrderStatus(int restaurantId, int orderId, OrderStatusEnum newStatus);
        // Task CancelOrderAsync(int restaurantId, int orderId, OrderStatusEnum newStatus);
    }
}