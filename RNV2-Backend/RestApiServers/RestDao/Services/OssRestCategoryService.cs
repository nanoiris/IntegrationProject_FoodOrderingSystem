using Microsoft.EntityFrameworkCore;
using RestaurantDao.Contexts;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;

namespace RestaurantDao.Services
{
    public partial class RestaurantService : IRestaurantService
    {
        public async Task<AppResult> AddCategory(RestCategory category)
        {
            using (var ctx = new RestaurantContext())
            {
               var result = ctx.RestCategories.Where(x => x.Name == category.Name).CountAsync()
                    .GetAwaiter().GetResult();
               if (result > 0)
                    return new AppResult("The category name already exists",false);
               category.Id = Guid.NewGuid().ToString("N");
               //category.PartionKey = "Restaurant";
               ctx.Add(category);
               result = await ctx.SaveChangesAsync();
               if (result == 1)
                    return new AppResult(category.Id, true); ;
                return new AppResult($"Add category failed,result = {result.ToString()}", false);
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
                    row.Name = category.Name;
                    row.Logo = category.Logo;
                    var result = await ctx.SaveChangesAsync();
                    if (result == 1)
                        return true;
                }
                return false;
            }
        }

        public async Task<bool> UpdateCategoryLogo(RestCategory model)
        {
            using (var ctx = new RestaurantContext())
            {
                var row = await ctx.RestCategories.FindAsync(model.Id);
                Console.WriteLine("UpdateCategoryLogo 1");
                if (row == null)
                    return false;
                Console.WriteLine("UpdateCategoryLogo 2");
                row.Logo = model.Logo;
                var result = await ctx.SaveChangesAsync();
                Console.WriteLine($"UpdateCategoryLogo 3 ={result}");
                return result == 1 ? true : false;
            }
        }
    }
}
