using System.Collections.Generic;
using System.Threading.Tasks;
using RmsApp.Dtos;

namespace RmsApp.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> ListCategoryAsync(string restaurantId);
        Task AddCategoryAsync(string restaurantId, CategoryDto category);
        Task<CategoryDto> GetCategoryAsync(string categoryId);
        Task UpdateCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(string categoryId);
    }
}