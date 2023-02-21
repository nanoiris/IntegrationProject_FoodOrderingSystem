using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.IServices
{
    public interface IRatingService
    {
        public Task<bool> RateToRest(RestRating rating);
        public Task<bool> RateToDelivery(DeliveryRating rating);
        public Task<bool> RateToMenuItem(MenuItemRating rating);

        public Task<string[]> CalculateRestRatings(string restaurantId);
        public Task<int> CalculateRestTotalRatings(string restaurantId);
    }
}
