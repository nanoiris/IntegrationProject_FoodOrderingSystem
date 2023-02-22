using Microsoft.EntityFrameworkCore;
using RestaurantDao.Contexts;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public partial class MenuService : IMenuService
    {
        public Task<bool> AddMenu(string restaurantId, MenuItem item)
        {
            using (var ctx = new RestaurantContext())
            {

            }
        }

        public Task<bool> DeleteMenu(string restaurantId, string menuId)
        {
            throw new NotImplementedException();
        }


        public async Task<MenuItem?> FindMenu(string restaurantId,string menuId)
        {
            using (var ctx = new RestaurantContext())
            {
                var category = await ctx.MenuCategories
                    .Where(x => x.RestaurantId == restaurantId)
                    .FirstAsync();
                return category.MenuItemList?.First(x => x.Id == menuId);
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

        public Task<List<MenuItem>?> SearchMenu(string restaurantId, string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMenu(MenuItem item)
        {
            throw new NotImplementedException();
        }
    }
}
