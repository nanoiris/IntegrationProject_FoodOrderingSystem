using Microsoft.EntityFrameworkCore;
using RestaurantDao.Contexts;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using System;

namespace RestaurantDao.Services
{
    public partial class RestaurantService : IRestaurantService
    {
        public async Task<bool> AddCategory(RestCategory category, Stream logo)
        {
            category.Id = Guid.NewGuid().ToString("N");
            category.PartionKey = "MenuCategory";
            using (var ctx = new RestaurantContext())
            {
               ctx.RestCategories.Add(category);
               var result = await ctx.SaveChangesAsync();
               if (result == 1)
                    return true;
                return false;
            }
        }

        public async Task<bool> DeleteCategory(string categoryId)
        {
            using (var ctx = new RestaurantContext())
            {
                RestCategory? row = await ctx.RestCategories.FirstAsync(x => x.Id == categoryId);
                    
                if (row != null)
                {
                    ctx.Remove(row);
                    var result = await ctx.SaveChangesAsync();
                    if(result == 1)
                        return true;
                }
                return false;
            }
        }

        public Task<RestCategory?> FindCategory(string categoryId)
        {
            using (var ctx = new RestaurantContext())
            {
                return ctx.RestCategories.FirstOrDefaultAsync(x => x.Id == categoryId);
            }
        }

        public Task<List<RestCategory>> ListCategory()
        {
            using (var ctx = new RestaurantContext())
            {
                return ctx.RestCategories.OrderBy(x => x.Name).ToListAsync();
            }
        }
        public async Task<bool> UpdateCategory(RestCategory category)
        {
            using (var ctx = new RestaurantContext())
            {
                RestCategory? row = await ctx.RestCategories.FirstAsync(x => x.Id == category.Id);

                if (row != null)
                {
                    row = category;
                    var result = await ctx.SaveChangesAsync();
                    if (result == 1)
                        return true;
                }
                return false;
            }
        }
    }
}
