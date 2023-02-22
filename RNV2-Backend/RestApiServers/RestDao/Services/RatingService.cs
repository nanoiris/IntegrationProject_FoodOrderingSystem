using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using RestaurantDao.Contexts;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public class RatingService : IRatingService
    {
        public async Task<string[]> CalculateRestRatings(string restaurantId)
        {
            string[] subtotals = new string[5];
            using (var ctx = new RatingContext())
            {
                int total = await ctx.RestRatings.CountAsync(x => x.RestaurantId == restaurantId);
                if (total == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        subtotals[i] = "0";
                    }
                    return subtotals;
                }
                for (int i = 1; i <= 5; i++)
                {
                    int subtotal = await ctx.RestRatings.CountAsync(x => x.RestaurantId == restaurantId && x.Value == i);

                    float percentage = subtotal * 100 / total;
                    subtotals[i - 1] = percentage.ToString("0") + "%";
                }
            }
            return subtotals;
        }

        public Task<int> CalculateRestTotalRatings(string restaurantId)
        {
            using (var ctx = new RatingContext())
            {
                return ctx.RestRatings.CountAsync(x => x.RestaurantId == restaurantId);
            }
        }

        public async Task<bool> RateToDelivery(DeliveryRating rating)
        {
            using(var ctx = new RatingContext())
            {
                rating.Id = Guid.NewGuid().ToString("N");
                //rating.PartionKey = "Delivery";
                ctx.Add(rating);
                var result = await ctx.SaveChangesAsync();
                if (result == 1)
                    return true;
                return false;
            }
        }

        public async Task<bool> RateToMenuItem(MenuItemRating rating)
        {
            using (var ctx = new RatingContext())
            {
                rating.Id = Guid.NewGuid().ToString("N");
                //rating.PartionKey = "MenuItem";
                ctx.MenuRatings.Add(rating);
                var result = await ctx.SaveChangesAsync();
                if (result == 1)
                    return true;
                return false;
            }
        }

        public async Task<bool> RateToRest(RestRating rating)
        {
            using (var ctx = new RatingContext())
            {
                rating.Id = Guid.NewGuid().ToString("N");
               // rating.PartionKey = "Restaurant";
                ctx.RestRatings.Add(rating);
                var result = await ctx.SaveChangesAsync();
                if (result == 1)
                    return true;
                return false;
            }
        }
    }
}
