using Microsoft.EntityFrameworkCore;
using RestaurantDao.Contexts;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;

namespace RestaurantDao.Services
{
    public partial class MenuService : IMenuService
    {
        public async Task<bool> AddMenu(string restaurantId, MenuItem item)
        {
            using (var ctx = new RestaurantContext())
            {
                var category = await ctx.MenuCategories.FirstAsync(x => x.RestaurantId == restaurantId && x.Id == item.CategoryId);
                if(category == null)
                    return false;
                if (category!.MenuItemList == null)
                    category!.MenuItemList = new List<MenuItem>();
                category!.MenuItemList!.Add(item);
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }

        public async Task<bool> DeleteMenu(string categoryId, string menuId)
        {
            using (var ctx = new RestaurantContext())
            {
                var row = await ctx.MenuCategories
                    .Where(x => x.Id == categoryId)
                    .FirstAsync();

                var menuItem = row.MenuItemList?.Where(x => x.Id == menuId).First();
                if (menuItem == null)
                    return false;
                
                row.MenuItemList?.Remove(menuItem);
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        } 

        public async Task<MenuItem?> FindMenu(string categoryId,string menuId)
        {
            using (var ctx = new RestaurantContext())
            {
                var row = await ctx.MenuCategories
                    .Where(x => x.Id == categoryId)
                    .FirstAsync();
                
                return row.MenuItemList?.Find(x => x.Id == menuId);
            }
        }
        public async Task<List<MenuItem>?> GetFeaturedMenus(string restaurantId)
        {
            using (var ctx = new RestaurantContext())
            {
                var rows = await ctx.MenuCategories.Where(x => x.RestaurantId == restaurantId).ToListAsync();
                List<MenuItem>? resultList = new List<MenuItem>();
                foreach (var row in rows)
                {
                    var list = row.MenuItemList?.Where(x => x.IsFeatured == true).ToList();
                    if(list != null)
                        resultList.AddRange(list);
                }
                return resultList;
            }
        }  
        public Task<List<MenuCategory>> ListMenu(string restaurantId)
        {
            using (var ctx = new RestaurantContext())
            {
                return ctx.MenuCategories.Where(x => x.RestaurantId == restaurantId).ToListAsync();
            }
        }

        public async Task<List<MenuItem>?> SearchMenu(string restaurantId, string name)
        {
            using (var ctx = new RestaurantContext())
            {
                var rows = await ctx.MenuCategories
                    .Where(x => x.RestaurantId == restaurantId)
                    .ToListAsync();

                List<MenuItem> list = new List<MenuItem>();
                foreach (var row in rows)
                {
                    var item = row.MenuItemList.Find(x => x.Name.Contains(name));
                    if(item != null) list.Add(item);
                }
                return list;
            }
        }

        public async Task<bool> UpdateMenu(MenuItem item)
        {
            using (var ctx = new RestaurantContext())
            {
                var category = await ctx.MenuCategories.FindAsync(item.CategoryId);
                if (category == null)
                    return false;
                var row = category.MenuItemList?.Find(x => x.Id == item.Id);
                if(row == null) 
                    return false;

                if (!string.IsNullOrEmpty(item.Name))
                    row.Name = item.Name;
                if (!string.IsNullOrEmpty(item.Description))
                    row.Description = item.Description;
                if (!string.IsNullOrEmpty(item.CategoryId))
                    row.CategoryId = item.CategoryId;
                if(!string.IsNullOrEmpty(item.Logo))
                  row.Logo = item.Logo;

                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        } 
    }
}
