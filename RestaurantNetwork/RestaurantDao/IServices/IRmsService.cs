using RestaurantDao.Enums;
using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.IServices
{
    public interface IRmsService
    {
        public List<MenuCategory> ListCategry(int restaurantId);
        public MenuCategory? FindCategory(int restaurantId,int id);
        public void AddCategory(int restaurantId,MenuCategory category);
        public void UpdateCategory(int restaurantId,MenuCategory category);
        public void DeleteCategory(int restaurantId,int categoryId);

        public List<MenuItem>? ListFeaturedMenu(int restaurantId);
        public MenuItem? FindFeaturedMenuitem(int restaurantId,int id);
        
        public List<MenuItem>? ListMenu(int restaurantId);
        public MenuItem? FindMenu(int restaurantId,int menuId);
        public List<MenuItem>? SearchMenu(int restaurantId, string name);
        public void AddMenu(int restaurantId,MenuItem item, Stream logo);
        public void UpdateMenu(int restaurantId,MenuItem item, Stream logo);
        public void DeleteMenu(int restaurantId,int menuId);

        public Restaurant? FindRestaurant(int restaurantId); 
        public List<RestCategory> ListCategory();
        public void UpdateRestaurant(Restaurant restaurant, Stream logo);

        public Order FindOrderById(int restaurantId, int orderId);
        public void CancelOrder(int restaurantId, int orderId);
        
        public List<Order> ListOrderByStatus(int restaurantId,StatusEnum status);
        public List<Order> SearchOrder(int restaurantId, string email,StatusEnum status);

        public void UpdateOrderStatus(int restaurantId, int orderId, StatusEnum newStatus); 


    }
}
