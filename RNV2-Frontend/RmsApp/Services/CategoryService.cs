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
            _httpClient.BaseAddress = new Uri(Constants.RestUri);
            _flashMessageService = flashMessageService;
        }


        public List<CategoryDto> Categories { get; set; }

        public async Task<List<CategoryDto>> ListCategoryAsync(string restaurantId)
        {
            Console.WriteLine("Enter CategoryListService Log...");
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


        public async Task AddCategoryAsync(string restaurantId, CategoryDto category)
        {
            try
            {
                var newCategory = new CategoryDto
                {
                    Name = category.Name,
                    RestaurantId = restaurantId
                };
                var response = await _httpClient.PostAsJsonAsync("api/menucategory/NewOne", newCategory);
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
                Console.WriteLine(ex.Message, "Error while adding category.");
                _flashMessageService.FailureMessage = "Failed to add the category.";
            }
        }
        public async Task<CategoryDto> GetCategoryAsync(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
                throw new ArgumentException("Category ID cannot be null or empty.", nameof(categoryId));
            }
            var response = await _httpClient.GetAsync($"api/menucategory/one/{categoryId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CategoryDto>();
            }
            Console.WriteLine("Failed to get category with ID {CategoryId}. StatusCode: {StatusCode}", categoryId, response.StatusCode);
            throw new ApplicationException("Failed to get category.");
        }


        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto));
            }
            var response = await _httpClient.PutAsJsonAsync($"api/menucategory/updatedone/{categoryDto.Id}", categoryDto);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to update category with ID {CategoryId}. StatusCode: {StatusCode}", categoryDto.Id, response.StatusCode);
                throw new ApplicationException("Failed to update category.");
            }
        }




        public async Task DeleteCategoryAsync(string categoryId)
        {
            var response = await _httpClient.DeleteAsync($"api/menucategory/deletedone/{categoryId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to delete category with ID {CategoryId}. StatusCode: {StatusCode}", categoryId, response.StatusCode);
                throw new ApplicationException("Failed to delete category.");
            }
        }

    }


}