using Microsoft.AspNetCore.Identity;
using RestaurantDao.Enums;
using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.IServices
{
    public interface IOssService
    {
        
        public void AddRestaurantUser(AppUser user,Stream logo);
        public List<AppUser> ListRestaurantUsers(int restaurantId);
        public AppUser? FindRestaurantUser(int id);
        public void DeleteRestaurantUser(int id);
        public void UpdateRestaurantUser(AppUser user,Stream logo);

        public List<IdentityRole> ListRole();
        public void AddRole(string name);
        public void DeleteRole(string id);

        public void LockUser(string userId);
        public void FreeUser(string userId);

        public void ResetUserPassword(string email);

        public List<Order> SearchOrderByEmail(string email);
        public Order FindOrderById(int orderId);
        public void CancelOrder(int orderId);
        public List<Order> ListActiveOrder();
        
        public void AddRestaurant(Restaurant restaurant, Stream logo);
        public List<Restaurant> ListRestaurant();
        public Restaurant? FindRestaurant(int restaurantId);

        public List<RestCategory> ListCategory();
        public RestCategory? FindCategory(int categoryId);
        public void DeleteCategory(int categoryId);
        public void AddRestaurantCategory(RestCategory category, Stream logo);
        public void DeleteRestaurant(int restaurantId);
        public void UpdateRestaurant(Restaurant restaurant, Stream logo);
        

    }
}
