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


        public async Task<ItemEditDto> GetItemAsync(string categoryId, string itemId)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
                throw new ArgumentException("Category ID cannot be null or empty.", nameof(categoryId));
            }
            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentException("Item ID cannot be null or empty.", nameof(itemId));
            }

            var response = await _httpClient.GetAsync($"api/menu/one/{categoryId}/{itemId}");

            if (response.IsSuccessStatusCode)
            {
                var item = await response.Content.ReadFromJsonAsync<ItemEditDto>();
                return item;
            }

            Console.WriteLine("Failed to get item with ID {ItemId} in category with ID {CategoryId}. StatusCode: {StatusCode}", itemId, categoryId, response.StatusCode);
            throw new ApplicationException($"Failed to get item with ID {itemId} in category with ID {categoryId}.");
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
                Console.WriteLine("Failed to get categories. Status code: {0}", response.StatusCode);
            }

            return Categories;
        }

        public async Task UpdateItemAsync(ItemEditDto menuItemDto)
        {
            if (menuItemDto == null)
            {
                throw new ArgumentNullException(nameof(menuItemDto));
            }
            var response = await _httpClient.PutAsJsonAsync($"api/menu/updateone", menuItemDto);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to update menu item with ID {MenuItemId}. StatusCode: {StatusCode}", menuItemDto.Id, response.StatusCode);
                throw new ApplicationException("Failed to update menu item.");
            }
        }

        public async Task DeleteItemAsync(string categoryId, string id)
        {
            var response = await _httpClient.DeleteAsync($"api/menu/deleteOne/{categoryId}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to delete menu item with ID {ItemId}. StatusCode: {StatusCode}", id, response.StatusCode);
                throw new ApplicationException("Failed to delete menu item.");
            }
        }


    }
}