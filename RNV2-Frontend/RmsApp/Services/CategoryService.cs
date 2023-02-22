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


namespace RmsApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger _logger;
        private readonly IFlashMessageService _flashMessageService;

        public CategoryService(HttpClient httpClient, IFlashMessageService flashMessageService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5064/");
            _flashMessageService = flashMessageService;
        }


        public List<CategoryDto> Categories { get; set; }

        // public async Task<List<CategoryDto>> ListCategoryAsync(int restaurantId)
        // {
        //     Console.WriteLine("Enter CategoryListservice Log...");
        //     Categories = new List<CategoryDto>();
        //     return await _httpClient.GetFromJsonAsync<List<CategoryDto>>("http://localhost:5000/mock/mockCategory.json");
        // }
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

        public async Task GetCategoryAsync(string restaurantId, string categoryId)
        {
            // Console.WriteLine("Enter categortGet service Log...");
        }

        public async Task AddCategoryAsync(string restaurantId, CategoryDto category)
        {
            try
            {
                var newCategory = new CategoryDto
                {
                    Name = category.Name,
                    RestaurantId = restaurantId
                };
                var response = await _httpClient.PostAsJsonAsync("api/category/NewOne", newCategory);
                if (response.IsSuccessStatusCode)
                {
                    _flashMessageService.SuccessMessage = "Category added successfully.";
                    // NavigationManager.NavigateTo("/category");
                }
                else
                {
                    _flashMessageService.FailureMessage = "Failed to add the category.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding category.");
                _flashMessageService.FailureMessage = "Failed to add the category.";
            }
        }




        public async Task EditCategoryAsync(string restaurantId, string categoryId)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteCategoryAsync(string restaurantId, string categoryId)
        {
            throw new NotImplementedException();
        }


    }


}