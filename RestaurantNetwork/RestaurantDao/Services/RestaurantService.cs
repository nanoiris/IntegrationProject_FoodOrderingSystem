using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RestaurantDao.Services
{
    public class RestaurantService : IRestarantService
    {
        public Restaurant GetRestById(int id)
        {
            using(var db = new AppDbContext())
            {
                Restaurant? rest = db.Restaurants.Include("Category").FirstOrDefault(x => x.Id == id);
                
                rest.MenuCategories = db.MenuCategories.Include("Menus").Where(x => x.Owner.Id == id).ToList();
                rest.Ratings = db.Ratings.Where(x => x.Target.Id == id).ToList();
                return rest;
            }
        }

        public List<RestCategory> ListCategory()
        {
            using (var db = new AppDbContext())
            {
                return db.RestCategoris.OrderBy(x => x.Id).ToListAsync().GetAwaiter().GetResult();
            }
        }

        public List<Restaurant> ListWeeklyTrends()
        {
            using (var db = new AppDbContext())
            {
                return db.Restaurants.Where(x => x.Featured == Enums.FeaturedEnum.Yes).OrderBy(x => x.Id)
                    .ToListAsync().GetAwaiter().GetResult();
            }
        }

        public List<Restaurant> ListWithLimit(int limit)
        {
            using (var db = new AppDbContext())
            {
                return db.Restaurants.Take(limit).OrderBy(x => x.Id).ToListAsync().GetAwaiter().GetResult();
            }
        }

        public List<Restaurant> Search(string searchKey, string categoryName, string sortField)
        {
            if(searchKey.IsNullOrEmpty() && categoryName.IsNullOrEmpty() && sortField.IsNullOrEmpty())
            {
                return ListWithLimit(20);
            }
            
            using (var db = new AppDbContext())
            {
                Expression<Func<Restaurant, bool>> expression = null;
                if (searchKey != null)
                {
                    expression = x => x.Name.ToLower().Contains(searchKey.ToLower());
                    if(categoryName != null)
                    {
                        expression = expression.And(x => x.Category.Name == categoryName);
                    }
                }
                else
                {
                    expression = x => x.Category.Name == categoryName;
                }
                
                var ds = db.Restaurants.Where(expression).OrderBy(x => x.Name);
                return ds.ToListAsync().GetAwaiter().GetResult();
            }
        }
        public List<MenuItem> GetFeaturedMenus(int restaurantId)
        {
            using (var db = new AppDbContext())
            {
                return db.Menus.Where(x => x.Owner.Id == restaurantId && x.Featured == Enums.FeaturedEnum.Yes)
                    .OrderBy(x => x.Name).ToListAsync()
                    .GetAwaiter().GetResult();
            }
        }
        public string[] CalculateRatings(int restaurantId)
        {
            string[] subtotals = new string[5];
            using (var db = new AppDbContext())
            {
                int total = db.Ratings.Where(x => x.Target.Id == restaurantId).Count();
                if(total == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        subtotals[i] = "0";
                    }
                    return subtotals;
                }
                for(int i = 1; i <= 5; i++) 
                {
                    float subtotal = db.Ratings.Where(x => x.Target.Id == restaurantId && x.Value == i).Count() * 100 / total;
                    subtotals[i - 1] = subtotal.ToString("0") + "%"; 
                }
            }
            return subtotals;
        }
        public int CalculateTotalRatings(int restaurantId)
        {
            using (var db = new AppDbContext())
            {
                return db.Ratings.Where(x => x.Target.Id == restaurantId).Count();
            }
        } 
    }
}
