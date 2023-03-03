using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data.model
{
    public class AddToCartModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isFeatured { get; set; }
        public decimal price { get; set; }
        public string logo { get; set; }
        public string categoryId { get; set; }
    }
}
