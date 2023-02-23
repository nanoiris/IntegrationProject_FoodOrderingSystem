using Microsoft.Extensions.Logging;
using RestaurantDaoBase.Models;
using RestaurantNetowrkApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MenuItem = RestaurantDaoBase.Models.MenuItem;

namespace RestaurantNetowrkApp.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;


        public RestaurantService(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5064/");
            _logger = logger;
        }

        public Task<List<MenuItem>> GetFeaturedMenus(string restaurantId)
        {
            throw new NotImplementedException();
        }

        public Task<Restaurant> GetRestById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RestCategory>> ListCategory()
        {
            var Categories = new List<RestCategory>();
            return await _httpClient.GetFromJsonAsync<List<RestCategory>>($"api/restcategory/list");
            //HttpResponseMessage response = await _httpClient.GetAsync($"api/restcategory/list");
            //if(response.IsSuccessStatusCode)
            //{
            //   Categories = await response.Content.ReadFromJsonAsync<List<RestCategory>>();
            //}
            //else
            //{
            //    _logger.LogError("Failed to get categories. Status code: {0}", response.StatusCode);
            //}
            //return Categories;
        }

        public Task<List<Restaurant>> ListWeeklyTrends()
        {
            throw new NotImplementedException();
        }

        public Task<List<Restaurant>> ListWithLimit(int limit)
        {
            throw new NotImplementedException();
        }

        public Task<List<Restaurant>> Search(string searchKey, string categoryName)
        {
            throw new NotImplementedException();
        }
    }
}
