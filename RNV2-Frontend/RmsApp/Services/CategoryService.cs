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
        /*
        public CategoryService() 
        {

        }
        public CategoryService(HttpClient httpClient, ILogger _logger)
        {
            _httpClient = httpClient;
            this._logger = _logger; 
        }
        public CategoryService(ILogger _logger)
        {
            this._logger = _logger;
        }
        */

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<CategoryDto> Categories { get; set; }

        public async Task<List<CategoryDto>> ListCategoryAsync(int restaurantId)
        {
            Console.WriteLine("Enter CategoryListservice Log...");
            Categories = new List<CategoryDto>();
            return await _httpClient.GetFromJsonAsync<List<CategoryDto>>("http://localhost:5000/mock/mockCategory.json");
        }
        public async Task GetCategoryAsync(int restaurantId, int categoryId)
        {
            // Console.WriteLine("Enter categortGet service Log...");
        }

        public async Task AddCategoryAsync(int restaurantId, CategoryDto category)
        {
            throw new NotImplementedException();
        }


        public async Task EditCategoryAsync(int restaurantId, int categoryId)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteCategoryAsync(int restaurantId, int categoryId)
        {
            throw new NotImplementedException();
        }


    }


}