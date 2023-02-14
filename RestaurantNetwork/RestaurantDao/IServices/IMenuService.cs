using RestaurantDao.Enums;
using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.IServices
{
    public interface IMenuService
    {
        public void Rate(string email, int restaurantId,int score);
        public Restaurant FindRestByMenuItem(int menuItemID);
        public MenuItem GetMenuItem(int menuItemID);
    }
}
