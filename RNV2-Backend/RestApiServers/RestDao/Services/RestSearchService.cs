using Microsoft.EntityFrameworkCore;
using RestaurantDao.Contexts;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public partial class RestaurantService : IRestaurantService
    {
        public Task<List<Restaurant>> ListWeeklyTrends()
        {
            using (var ctx = new RestaurantContext())
            {
                //var today = DateTime.Now;
                return ctx.Restaurants.Where(x => x.IsFeatured == true).ToListAsync();
            }
        }
        public Task<List<Restaurant>> ListWithLimit(int limit)
        {
            using (var ctx = new RestaurantContext())
            {
                return ctx.Restaurants.Take(limit).ToListAsync();
            }
        }
        public Task<List<Restaurant>> Search(string searchKey, string categoryId)
        {
            using (var ctx = new RestaurantContext())
            {
                return search(ctx, searchKey, categoryId);
            }
        }
        private Task<List<Restaurant>> search(RestaurantContext ctx, string searchKey, string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
                if (string.IsNullOrEmpty(searchKey))
                    return ctx.Restaurants.Take(20).ToListAsync();
                return ctx.Restaurants.Take(20).Where(x => x.Name.Contains(searchKey))
                    .ToListAsync();
            }
            if (string.IsNullOrEmpty(searchKey))
                return ctx.Restaurants.Take(20).Where(x => x.CategoryId == categoryId)
                    .ToListAsync();
            else
                return ctx.Restaurants.Take(20).Where(x => x.CategoryId == categoryId && x.Name.Contains(searchKey))
                    .ToListAsync();
            /*
            var query = from Restaurants in ctx.Restaurants
                        select Restaurants;

            Expression<Func<Restaurant, bool>> expression = t => true;
            if (!string.IsNullOrEmpty(categoryId))
            {
                expression = expression.And(t => t.CategoryId == categoryId);
            }
            if (!string.IsNullOrEmpty(searchKey))
            {
                expression = expression.And(t => t.Name.Contains(searchKey));
            }
            var ds = query.AsQueryable().Where(expression).OrderBy(x => x.Name);
            return ds.ToListAsync();
            */
        }
    }
}
