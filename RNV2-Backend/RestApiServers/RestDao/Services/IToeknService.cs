using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public interface IToeknService
    {
        public string GetRole();
        public string GetUserName();
        public string GetRestaurant();
    }
}
