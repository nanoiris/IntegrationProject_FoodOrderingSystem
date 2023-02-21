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
using Microsoft.Extensions.Logging;

namespace RmsApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger _logger;

        public CategoryService(HttpClient httpClient, ILogger _logger)
        {
            _httpClient = httpClient;
            this._logger = _logger; 
        }
        public CategoryService( ILogger _logger)
        {
            this._logger = _logger;
        }
        public List<CategoryDto> Categories { get; set; }
        // public CategoryService()
        // {
        //     Categories = new List<CategoryDto>();
        // }


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
            _logger.LogInformation("Enter service Log...");
            restaurantId = 1;
            Categories = new List<CategoryDto>();

            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5000/mock/mockCategory.json");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Categories = JsonSerializer.Deserialize<List<CategoryDto>>(content,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }
                else
                {
                    Debug.WriteLine(@"\tERROR {0}", response.ReasonPhrase);
                }
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