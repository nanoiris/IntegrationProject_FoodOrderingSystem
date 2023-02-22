using System.Collections.Generic;
using System.Threading.Tasks;
using RmsApp.Dtos;

namespace RmsApp.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> ListCategoryAsync(string restaurantId);
        Task GetCategoryAsync(string restaurantId, string categoryId);
        Task AddCategoryAsync(string restaurantId, CategoryDto category);
        Task EditCategoryAsync(string restaurantId, string categoryId);
        Task DeleteCategoryAsync(string restaurantId, string categoryId);
    }
}