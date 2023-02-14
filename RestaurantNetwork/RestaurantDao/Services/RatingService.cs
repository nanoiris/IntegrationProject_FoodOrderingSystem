using RestaurantDao.IServices;
using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public class RatingService : IRatingService
    {
        public void AddRating(Rating rating, int restId)
        {
            using (var db = new AppDbContext())
            {
                rating.Target = db.Restaurants.FirstOrDefault(r => r.Id == restId);
                db.Ratings.Add(rating);
                db.SaveChangesAsync().GetAwaiter().GetResult();
            }
        }

        public void CalculateRating(int restId)
        {
            using (var db = new AppDbContext())
            {
                Restaurant rest = db.Restaurants.FirstOrDefault(r => r.Id == restId);
                int sum = 0;
                foreach(var rating in rest.Ratings)
                {
                    sum += rating.Value;
                }
                rest.Rating = sum/rest.Ratings.Count();

            }
        }

    }
}
