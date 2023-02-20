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
        public void RateToRest(RestRating rating);
        public void RateToDelivery(DeliveryRating rating);
        public void RateToMenuItem(MenuItemRating rating);

        public string[] CalculateRestRatings(int restaurantId);
        public int CalculateRestTotalRatings(int restaurantId);
    }
}
