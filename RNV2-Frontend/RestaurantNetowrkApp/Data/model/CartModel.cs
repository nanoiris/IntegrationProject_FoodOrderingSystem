using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantNetowrkApp.Data.Dto;

namespace RestaurantNetowrkApp.Data.model
{
    public class CartModel
    {
        public string email { get; set; }
        public MenuItemDto menuItem { get; set; }
        public string restaurantId { get; set; }
        public string restaurantName { get;set; }
    }
}
