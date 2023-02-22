using Microsoft.EntityFrameworkCore;
using RestaurantDao.Contexts;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;

namespace RestaurantDao.Services
{
    public partial class MenuService : IMenuService
    {
        public async Task<bool> AddCategory(MenuCategory category)
        {
            using (var ctx = new RestaurantContext())
            {
                category.Id = Guid.NewGuid().ToString("N");
                ctx.MenuCategories.Add(category);
                var result = await ctx.SaveChangesAsync();
                if(result == 1)
                    return true;
                return false;
            }
        }
        public async Task<bool> DeleteCategory(string categoryId)
        {
            using (var ctx = new RestaurantContext())
            {
                var row = await ctx.MenuCategories.Where(x => x.Id == categoryId)
                    .FirstAsync();
                if (row == null)
                    return false;
                ctx.MenuCategories.Remove(row);
                var result = await ctx.SaveChangesAsync();
                if (result == 1)
                    return true;
                return false;
            }
        }
        public Task<MenuCategory> FindCategory(string id)
        {
            using (var ctx = new RestaurantContext())
            {
                return ctx.MenuCategories.Where(x => x.Id == id)
                    .FirstAsync();
            }
        }
        public Task<List<MenuCategory>> ListCategry(string restaurantId)
        {
            using (var ctx = new RestaurantContext())
            {
                return ctx.MenuCategories.Where(x => x.RestaurantId == restaurantId)
                    .ToListAsync();
            }
        }

        public async Task<bool> UpdateCategory(MenuCategory category)
        {
            using (var ctx = new RestaurantContext())
            {
                var row = await ctx.MenuCategories.Where(x => x.Id == category.Id).FirstAsync();
                if(row == null) return false;

                row.Name = category.Name;
                row.RestaurantId = category.RestaurantId;
                row.MenuItemList = category.MenuItemList;
                var result = await ctx.SaveChangesAsync();
                if(result == 1)
                    return true;
                return false;
            }
        }
    }
}
