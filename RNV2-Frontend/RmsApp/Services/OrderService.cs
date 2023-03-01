// using System;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Collections.Generic;
// using System.Net.Http;
// using System.Text;
// using System.Text.Json;
// using RmsApp.Dtos;
// using RmsApp.Services;
// using System.Diagnostics;
// using System.Net.Http.Json;
// using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Components;
// using RestaurantDaoBase.Enums;

// namespace RmsApp.Services
// {
//     public class OrderService : IOrderService
//     {
//         private readonly HttpClient _httpClient;
//         private readonly ILogger _logger;
//         private readonly IFlashMessageService _flashMessageService;

//         public OrderService(HttpClient httpClient, IFlashMessageService flashMessageService)
//         {
//             _httpClient = httpClient;
//             _httpClient.BaseAddress = new Uri(Constants.RestUri);
//             _flashMessageService = flashMessageService;
//         }

//         public Task<List<OrderViewDto>> ListCategoryAsync()
//         {
//             throw new NotImplementedException();
//         }

//         public async Task<List<OrderDto>> ListOrderAsync(string restaurantId, StatusEnum status)
//         {
//             List<OrderViewDto> orders = new List<OrderViewDto>();
//             try
//             {
//                 HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/orders/list-by-restaurant-and-status", new RestAndStatusViewModel { RestaurantId = restaurantId, Status = status });
//                 if (response.IsSuccessStatusCode)
//                 {
//                     orders = await response.Content.ReadFromJsonAsync<List<OrderViewDto>>();
//                 }
//                 else
//                 {
//                     _logger.LogError("Failed to get orders. Status code: {0}", response.StatusCode);
//                 }
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError("Failed to get orders. Error: {0}", ex.Message);
//             }

//             return orders;
//         }

//     }


// }