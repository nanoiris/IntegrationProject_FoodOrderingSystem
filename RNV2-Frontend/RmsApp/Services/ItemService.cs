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
    public class ItemService : IItemService
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger _logger;
        private readonly IFlashMessageService _flashMessageService;

        public ItemService(HttpClient httpClient, IFlashMessageService flashMessageService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5064/");
            _flashMessageService = flashMessageService;
        }

        public List<CategoryDto> Categories { get; set; }


        public async Task AddItemAsync(string restaurantId, ItemAddDto menuItem)
        {
            try
            {
                Console.WriteLine("start add menu...");
                var multipartContent = new MultipartFormDataContent();

                // multipartContent.Add(new StringContent("test4"), "Name");

                // Console.WriteLine("test");
                // multipartContent.Add(new StringContent("test3"), "Description");
                // multipartContent.Add(new StringContent("2"), "Price");
                // multipartContent.Add(new StringContent("cf7faf91419e4a0099c58d832402da84"), "CategoryId");
                // multipartContent.Add(new StringContent(restaurantId), "RestaurantId");
                multipartContent.Add(new StringContent(menuItem.Name), "Name");
                Console.WriteLine("name is: " + menuItem.Name);
                multipartContent.Add(new StringContent(menuItem.Description), "Description");
                multipartContent.Add(new StringContent(menuItem.Price.ToString()), "Price");
                Console.WriteLine("price is: " + menuItem.Price);
                multipartContent.Add(new StringContent(restaurantId), "RestaurantId");
                Console.WriteLine("restaurantId is: " + restaurantId);
                multipartContent.Add(new StringContent(menuItem.CategoryId), "CategoryId");
                Console.WriteLine("categoryId is: " + menuItem.CategoryId);
                multipartContent.Add(new StringContent(menuItem.IsFeatured.ToString()), "IsFeatured");
                Console.WriteLine("is featured is: " + menuItem.IsFeatured);
                if (!string.IsNullOrEmpty(menuItem.Logo))
                {
                    byte[] imageBytes = Convert.FromBase64String(menuItem.Logo);
                    ByteArrayContent imageContent = new ByteArrayContent(imageBytes);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    multipartContent.Add(imageContent, "Image", "image.jpg");
                }

                // multipartContent.Add(new StreamContent(menuItem.UploadImg.OpenReadStream()), "Image", menuItem.UploadImg.FileName);
                // var imageContent = new StreamContent(menuItem.UploadImg.OpenReadStream; 
                var response = await _httpClient.PostAsync($"api/menu/NewOne/{restaurantId}", multipartContent);
                Console.WriteLine("add menu, after post");
                if (response.IsSuccessStatusCode)
                {
                    _flashMessageService.SuccessMessage = "Menu item added successfully.";
                    // NavigationManager.NavigateTo("/menuitem");
                }
                else
                {
                    _flashMessageService.FailureMessage = "Failed to add the menu item.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _flashMessageService.FailureMessage = "Failed to add the menu item.";
            }
        }

        public Task DeleteItemAsync(string itemId)
        {
            throw new NotImplementedException();
        }

        public Task<ItemEditDto> GetItemAsync(string itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryDto>> ListCategoryAsync(string restaurantId)
        {
            Console.WriteLine("Enter CategoryListService Log...");
            Categories = new List<CategoryDto>();
            HttpResponseMessage response = await _httpClient.GetAsync($"api/MenuCategory/List/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                //parse the JSON response into a list of CategoryDto objects
                Categories = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();
            }
            else
            {
                _logger.LogError("Failed to get categories. Status code: {0}", response.StatusCode);
            }

            return Categories;
        }




        public Task UpdateItemAsync(ItemEditDto itemEditDto)
        {
            throw new NotImplementedException();
        }
    }
}