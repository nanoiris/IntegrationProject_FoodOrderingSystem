using System;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RmsApp.Dtos;
using RmsApp.Services;

namespace RmsApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // public asyc Task<IEnumerable<OrderListDto>> ListActiveOrderAsync(int restaurantId)
        // {
        //     return await JsonSerializer.DeserializeAsync<IEnumerable<OrderListDto>>(
        //         await _httpClient.GetStreamAsync("api/restaurant/1/employee"),
        //         new JsonSerializerOptions
        //         {
        //             PropertyNameCaseInsensitive = true
        //         });

        // }
        // Task<OrderDetailDto> FindOrderByIdAsync(int restaurantId, int orderId);
        // Task UpdateOrderStatus(int restaurantId, int orderId, OrderStatusEnum newStatus);
        // Task CancelOrderAsync(int restaurantId, int orderId, OrderStatusEnum newStatus);

    }


}