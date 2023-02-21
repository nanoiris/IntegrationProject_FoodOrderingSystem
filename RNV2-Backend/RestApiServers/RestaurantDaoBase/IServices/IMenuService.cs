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
        public List<MenuItem> GetFeaturedMenus(string restaurantId);

        public List<MenuCategory> ListCategry(string restaurantId);
        public MenuCategory? FindCategory(string restaurantId, string id);
        public void AddCategory(string restaurantId, MenuCategory category);
        public void UpdateCategory(string restaurantId, MenuCategory category);
        public void DeleteCategory(string restaurantId, string categoryId);

        public List<MenuItem>? ListMenu(string restaurantId);
        public MenuItem? FindMenu(string restaurantId, string menuId);
        public List<MenuItem>? SearchMenu(string restaurantId, string name);
        public void AddMenu(string restaurantId, MenuItem item, Stream logo);
        public void UpdateMenu(string restaurantId, MenuItem item, Stream logo);
        public void DeleteMenu(string restaurantId, string menuId);

    }
}
