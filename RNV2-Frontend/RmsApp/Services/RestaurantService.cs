using System;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RestaurantDaoBase.Models;
using RmsApp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

namespace RmsApp.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger _logger;
        private readonly IFlashMessageService _flashMessageService;

        public RestaurantService(HttpClient httpClient, IFlashMessageService flashMessageService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5064/");
            _flashMessageService = flashMessageService;
        }

        public Restaurant Restaurant { get; set; }

        public async Task<Restaurant?> GetOneRestaurantAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Restaurant ID cannot be null or empty.", nameof(id));
            }
            var response = await _httpClient.GetAsync($"api/restaurant/one/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Restaurant>();
            }
            Console.WriteLine("Failed to get restaurant with ID {RestaurantId}. StatusCode: {StatusCode}", id, response.StatusCode);
            return null;
        }


        public async Task<Restaurant?> UpdateRestaurantAsync(RestaurantForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            var response = await _httpClient.PutAsJsonAsync($"api/restaurant/updatedone", form);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to update restaurant with ID {RestaurantId}. StatusCode: {StatusCode}", form.Id, response.StatusCode);
                throw new ApplicationException("Failed to update restaurant.");
            }

            return await response.Content.ReadFromJsonAsync<Restaurant>();
        }


    }


}