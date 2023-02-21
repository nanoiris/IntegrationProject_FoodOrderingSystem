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
        public List<Restaurant> ListWeeklyTrends()
        {
            throw new NotImplementedException();
        }
        public List<Restaurant> ListWithLimit(int limit)
        {
            throw new NotImplementedException();
        }
        public List<Restaurant> Search(string searchKey, string categoryName, string sortField)
        {
            throw new NotImplementedException();
        }
    }
}
