using System.Collections.Generic;
using System.Threading.Tasks;
using RmsApp.Dtos;

namespace RmsApp.Services
{
    public interface IItemService
    {
        Task<List<CategoryDto>> ListCategoryAsync(string restaurantId);
        Task AddItemAsync(string restaurantId, ItemAddDto menuItem);
        Task<ItemEditDto> GetItemAsync(string itemId);
        Task UpdateItemAsync(ItemEditDto itemEditDto);
        Task DeleteItemAsync(string itemId);
        // SearchByname
        // FeaturedList 
    }
}