using RestaurantDaoBase.Enums;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.IServices
{
    public interface IMenuService
    {
        public Task<List<MenuItem>?> GetFeaturedMenus(string restaurantId);

        public Task<List<MenuCategory>> ListCategory(string restaurantId);
        public Task<MenuCategory?> FindCategory(string id);
        public Task<bool> AddCategory(MenuCategory category);
        public Task<bool> UpdateCategory(MenuCategory category);
        public Task<bool> DeleteCategory(string categoryId);

        public Task<MenuCategory> ListMenu(string restaurantId,string categoryId);
        public Task<MenuItem?> FindMenu(string categoryId,string menuId);
        public Task<List<MenuItem>?> SearchMenu(string restaurantId, string name);
        public Task<bool> AddMenu(string restaurantId, MenuItem item);
        public Task<bool> UpdateMenu(MenuItem item);
        public Task<bool> DeleteMenu(string categoryId, string menuId);
    }
}
