using Microsoft.EntityFrameworkCore;
using RestaurantDao.Enums;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    internal class MenuService : IMenuService
    {
      
        public Restaurant FindRestByMenuItem(int menuItemID)
        {
            using (var db = new AppDbContext())
            {
                var row = db.Menus.Include("Owner").FirstOrDefault(x => x.Id == menuItemID);
                return row.Owner;
            }
        }

        public MenuItem GetMenuItem(int menuItemID)
        {
            using (var db = new AppDbContext())
            {
                return db.Menus.Find(menuItemID);
            }
        }  
        public void Rate(string email, int restaurantId, int score)
        {
            using (var db = new AppDbContext())
            {
                Restaurant target = db.Restaurants.Find(restaurantId);
                Rating rating = new Rating
                {
                    Target = target,
                    Value = score,
                    CreateBy = email,
                    CreateTime = DateTime.Now,
                };
                db.Ratings.Add(rating);
                db.SaveChanges();
            }
        } 
    }
}
