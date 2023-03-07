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
using RmsApp.Dtos;
using RmsApp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;

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
            _httpClient.BaseAddress = new Uri(Constants.RestUri);
            _flashMessageService = flashMessageService;
        }

        public RestaurantDto Restaurant { get; set; }

        public async Task<RestaurantDto?> GetOneRestaurantAsync(string id)
        {
            var response = await _httpClient.GetAsync($"api/restaurant/one/{id}");
            Console.WriteLine("rest id is" + id);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    Console.WriteLine("test");
                    return await response.Content.ReadFromJsonAsync<RestaurantDto>();

                }
                catch (JsonException ex)
                {
                    // Console.WriteLine("Failed to deserialize response: {ErrorMessage}", ex.Message);
                    return null;
                }
            }
            Console.WriteLine("Failed to get restaurant with ID {id}. StatusCode: {StatusCode}");

            return null;
        }



        public async Task UpdateRestaurantAsync(RestaurantDto restaurant)
        {
            try
            {
                Console.WriteLine("start edit menu...");
                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new StringContent(restaurant.Id), "id");
                multipartContent.Add(new StringContent(restaurant.Name), "Name");
                multipartContent.Add(new StringContent(restaurant.Description), "Description");
                multipartContent.Add(new StringContent(restaurant.PhoneNo), "PhoneNo");
                multipartContent.Add(new StringContent(restaurant.Address.Street), "Street");
                multipartContent.Add(new StringContent(restaurant.Address.City), "City");
                multipartContent.Add(new StringContent(restaurant.Address.State), "State");
                multipartContent.Add(new StringContent(restaurant.Address.PostalCode), "PostalCode");
                multipartContent.Add(new StringContent(restaurant.Address.Country), "Country");
                multipartContent.Add(new StringContent(restaurant.CategoryId), "CategoryId");
                multipartContent.Add(new StringContent(restaurant.Email), "email");
                multipartContent.Add(new StringContent(restaurant.IsFeatured.ToString()), "IsFeatured");
                Console.WriteLine("is featured is: " + restaurant.IsFeatured);
                Console.WriteLine("uploadimg is: " + restaurant.UploadImg?.Name);
                // below are the 3 line for attach images 
                var img = new StreamContent(restaurant.UploadImg?.OpenReadStream());
                img.Headers.ContentType = new MediaTypeHeaderValue(restaurant.UploadImg.ContentType);
                multipartContent.Add(content: img, "UploadImg", fileName: restaurant.UploadImg.Name);
                // above are the 3 line for attach images 
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
                var response = await _httpClient.PutAsync($"api/restaurant/updatedone", multipartContent);
                Console.WriteLine("update rest, after PUT");
                if (response.IsSuccessStatusCode)
                {
                    _flashMessageService.SuccessMessage = "rest update successfully...";
                }
                else
                {
                    _flashMessageService.FailureMessage = "Failed to update the rest...";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _flashMessageService.FailureMessage = "Failed to update the menu item...";
            }
        }


    }
}
