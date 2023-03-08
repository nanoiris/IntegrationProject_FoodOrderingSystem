using Microsoft.Extensions.Logging;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MenuItem = RestaurantDaoBase.Models.MenuItem;
using Serilog;
using RestaurantNetowrkApp.Data.Dto;
using System.Text.Json;

namespace RestaurantNetowrkApp.Services
{
    public class RestService
    {
        private HttpClient http;
        private string server;


        public RestService(string server)
        {
            this.server = server;
            http = new HttpClient();
            http.BaseAddress = new Uri(server);

            //http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
        }

        //public async Task<RestaurantDto> List(string listUrl)
        //{
        //    var response = await http.GetAsync(listUrl);
        //    List<T> rows = null;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        Log.Debug("RestService.List");
        //        try
        //        {
        //            rows = response.Content.ReadFromJsonAsync<List<T>>()
        //                .GetAwaiter().GetResult();
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Debug($"RestService.List : {ex.Message}");
        //        }
        //    }
        //    if (rows == null)
        //    {
        //        rows = new List<T>();
        //    }
        //    return rows;
        //}

        public Task<List<MenuItem>> GetFeaturedMenus(string restaurantId)
        {
            throw new NotImplementedException();
        }

        public RestaurantDto GetRestById(string id)
        {
           return http.GetFromJsonAsync<RestaurantDto>($"/api/restaurant/one/" + id).Result;

        }

        public async Task<List<RestCategoryDto>> ListRestCategory()
        {
            List<RestCategoryDto> restCategories= new List<RestCategoryDto>();
            var response = await http.GetAsync($"api/restcategory/list");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    restCategories = response.Content.ReadFromJsonAsync<List<RestCategoryDto>>()
                       .GetAwaiter().GetResult(); 
                }
                catch (Exception ex)
                {
                    Log.Debug($"RestService.get restaurant categories : {ex.Message}");
                }
            }
            return restCategories;
        }

        public async Task<List<RestaurantDto>> ListRestTrending()
        {
            List<RestaurantDto> restaurants = new List<RestaurantDto>();
            var response = await http.GetAsync($"api/restaurant/listweeklytrends");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    restaurants = response.Content.ReadFromJsonAsync<List<RestaurantDto>>()
                       .GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Log.Debug($"RestService.get restaurant trending : {ex.Message}");
                }
            }
            return restaurants;
        }


        public async Task<List<MenuCategoryDto>> ListMenuCategory(string id)
        {
            List<MenuCategoryDto> menuCategories = new List<MenuCategoryDto>();
            var response = await http.GetAsync($"api/MenuCategory/list/{id}");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    menuCategories = response.Content.ReadFromJsonAsync<List<MenuCategoryDto>>()
                       .GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Log.Debug($"RestService.get menu categories : {ex.Message}");
                }
            }
            return menuCategories;
        }

        public async Task<List<MenuItemDto>> ListFeaturedItem(string id)
        {
            List<MenuItemDto> menuItems = new List<MenuItemDto>();
            var response = await http.GetAsync($"api/MenuCategory/featuredlist/{id}");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    menuItems = response.Content.ReadFromJsonAsync<List<MenuItemDto>>()
                       .GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Log.Debug($"RestService.get featured menu items : {ex.Message}");
                }
            }
            return menuItems;
        }


        public Task<List<Restaurant>> ListWithLimit(int limit)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RestaurantDto>> Search(string searchKey, string categoryid)
        {
            var requestBody = JsonSerializer.Serialize(
                new
                {
                    searchKey = searchKey,
                    categoryid = categoryid
                }
            );
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Data.Constants.RestUri + "/api/Restaurant/Search"),
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json"),
            };
            var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<List<RestaurantDto>>(responseBody);

        }
    }
}
