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
            _httpClient.BaseAddress = new Uri(Constants.RestUri);
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
                multipartContent.Add(new StringContent(menuItem.CategoryId), "CategoryId");
                Console.WriteLine("categoryId is: " + menuItem.CategoryId);
                multipartContent.Add(new StringContent(menuItem.IsFeatured.ToString()), "IsFeatured");
                Console.WriteLine("is featured is: " + menuItem.IsFeatured);
                Console.WriteLine(menuItem.UploadImg?.Name);
                // below are the 3 line for attach images 
                var img = new StreamContent(menuItem.UploadImg?.OpenReadStream());
                img.Headers.ContentType = new MediaTypeHeaderValue(menuItem.UploadImg.ContentType);
                multipartContent.Add(content: img, "UploadImg", fileName: menuItem.UploadImg.Name);
                // above are the 3 line for attach images 
                var response = await _httpClient.PostAsync($"api/menu/NewOne/{restaurantId}", multipartContent);
                Console.WriteLine("add menu, after post");
                if (response.IsSuccessStatusCode)
                {
                    _flashMessageService.SuccessMessage = "Menu item added successfully...";
                }
                else
                {
                    _flashMessageService.FailureMessage = "Failed to add the menu item...";
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
            Console.WriteLine("get item service...");
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
            Console.WriteLine("Enter Category List Service Log...");
            Categories = new List<CategoryDto>();
            HttpResponseMessage response = await _httpClient.GetAsync($"api/MenuCategory/List/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                Categories = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();
            }
            else
            {
                Console.WriteLine("Failed to get categories. Status code: {0}", response.StatusCode);
            }

            return Categories;
        }

        public async Task UpdateItemAsync(ItemEditDto menuItem)
        {
            try
            {
                Console.WriteLine("start edit menu...");
                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new StringContent(menuItem.Id), "id");
                Console.WriteLine("ID is: " + menuItem.Id);
                multipartContent.Add(new StringContent(menuItem.Name), "Name");
                Console.WriteLine("name is: " + menuItem.Name);
                multipartContent.Add(new StringContent(menuItem.Description), "Description");
                multipartContent.Add(new StringContent(menuItem.Price.ToString()), "Price");
                Console.WriteLine("price is: " + menuItem.Price);
                // multipartContent.Add(new StringContent(restaurantId), "RestaurantId");
                multipartContent.Add(new StringContent(menuItem.CategoryId), "CategoryId");
                Console.WriteLine("categoryId is: " + menuItem.CategoryId);
                multipartContent.Add(new StringContent(menuItem.IsFeatured.ToString()), "IsFeatured");
                Console.WriteLine("is featured is: " + menuItem.IsFeatured);
                Console.WriteLine("uploadimg is: " + menuItem.UploadImg?.Name);
                // below are the 3 line for attach images 
                var img = new StreamContent(menuItem.UploadImg?.OpenReadStream());
                img.Headers.ContentType = new MediaTypeHeaderValue(menuItem.UploadImg.ContentType);
                multipartContent.Add(content: img, "UploadImg", fileName: menuItem.UploadImg.Name);
                // above are the 3 line for attach images 
                var response = await _httpClient.PutAsync($"api/menu/updatedone", multipartContent);
                Console.WriteLine("edit menu, after PUT");
                if (response.IsSuccessStatusCode)
                {
                    _flashMessageService.SuccessMessage = "Menu item update successfully...";
                }
                else
                {
                    _flashMessageService.FailureMessage = "Failed to update the menu item...";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _flashMessageService.FailureMessage = "Failed to update the menu item...";
            }
        }

        public async Task DeleteItemAsync(string categoryId, string id)
        {
            Console.WriteLine("Enter item delete service ...");
            var response = await _httpClient.DeleteAsync($"api/menu/deletedOne/{categoryId}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to delete menu item with ID {ItemId}. StatusCode: {StatusCode}", id, response.StatusCode);
                throw new ApplicationException("Failed to delete menu item.");
            }
        }
    }
}