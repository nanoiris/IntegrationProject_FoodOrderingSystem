using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public partial class RestaurantService : IRestaurantService
    {
        public void AddRestaurant(Restaurant restaurant, Stream logo)
        {
            throw new NotImplementedException();
        }

        public void DeleteRestaurant(string restaurantId)
        {
            throw new NotImplementedException();
        }

        public Restaurant? FindRestaurant(string restaurantId)
        {
            throw new NotImplementedException();
        }

        public List<Restaurant> ListRestaurant()
        {
            throw new NotImplementedException();
        }

        public void UpdateRestaurant(Restaurant restaurant, Stream logo)
        {
            throw new NotImplementedException();
        }
    }
}
