using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using RmsApp.Dtos;
using RmsApp.Services;
using System.Diagnostics;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using RestaurantDaoBase.Enums;

namespace RmsApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly IFlashMessageService _flashMessageService;

        public OrderService(HttpClient httpClient, IFlashMessageService flashMessageService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Constants.OrderUri);
            _flashMessageService = flashMessageService;
        }

        public async Task<List<OrderListDto>> ListOrderAsync(string restaurantId, int status)
        {
            List<OrderListDto> orders = new List<OrderListDto>();
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/order/ListByRestaurantAndStatus", new { restaurantId, status });
                if (response.IsSuccessStatusCode)

                {
                    Console.WriteLine("200 order status: ");
                    orders = await response.Content.ReadFromJsonAsync<List<OrderListDto>>();
                }
                else
                {
                    Console.WriteLine("Failed to get orders. Status code: {0}", response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to get orders. Error: {0}", ex.Message);
            }

            return orders;
        }
        public async Task<bool> UpdateOrderStatusAsync(string orderId, int status)
        {
            var model = new OrderStatusDto { OrderId = orderId, Status = status };
            var response = await _httpClient.PutAsJsonAsync("api/Order/OrderStatus", model);

            return response.IsSuccessStatusCode;
        }

        public async Task<OrderListDto> GetOrderAsync(string orderId)
        {
            var response = await _httpClient.GetAsync($"api/order/one/{orderId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderListDto>();


        }

    }

}