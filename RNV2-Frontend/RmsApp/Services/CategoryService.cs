using System;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RmsApp.Dtos;
using RmsApp.Services;

namespace RmsApp.Services
{
    public class CategoryService : ICategoryService
    {
        // private readonly HttpClient _httpClient;

        // public CategoryService(HttpClient httpClient)
        // {
        //     _httpClient = httpClient;
        // }
        public List<CategoryDto> Categories { get; set; }

        // public async Task<List<CategoryDto>> ListCategoryAsync(int restaurantId)
        // {
        //     Categories = new List<CategoryDto>();

        //     Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
        //     try
        //     {
        //         HttpResponseMessage response = await _client.GetAsync(uri);
        //         if (response.IsSuccessStatusCode)
        //         {
        //             string content = await response.Content.ReadAsStringAsync();
        //             Categories = JsonSerializer.Deserialize<List<CategoryDto>>(content,
        //             new JsonSerializerOptions
        //             {
        //                 PropertyNameCaseInsensitive = true
        //             });
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Debug.WriteLine(@"\tERROR {0}", ex.Message);
        //     }

        //     return Categories;
        // }
        public async Task<List<CategoryDto>> ListCategoryAsync(int restaurantId)
        {
            restaurantId = 1;
            Categories = new List<CategoryDto>();

            try
            {
                string content = await File.ReadAllTextAsync("wwwroot/mock/mockCategory");
                Categories = JsonSerializer.Deserialize<List<CategoryDto>>(content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Categories;
        }

        public Task EditCategory(int restaurantId, int categoryId)
        {
            throw new NotImplementedException();
        }
        public Task DeleteCategoryAsync(int restaurantId, int categoryId)
        {
            throw new NotImplementedException();
        }




    }


}