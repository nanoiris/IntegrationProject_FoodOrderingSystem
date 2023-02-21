using System.Collections.Generic;
using System.Threading.Tasks;
using RmsApp.Dtos;

namespace RmsApp.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> ListCategoryAsync(int restaurantId);
        Task EditCategory(int restaurantId, int categoryId);
        Task DeleteCategoryAsync(int restaurantId, int categoryId);
    }
}