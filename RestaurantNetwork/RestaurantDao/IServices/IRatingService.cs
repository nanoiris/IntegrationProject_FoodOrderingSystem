using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.IServices
{
    public interface IRatingService
    {
        public void AddRating(Rating rating, int restId);
        public void CalculateRating(int RestId);
    }
}
