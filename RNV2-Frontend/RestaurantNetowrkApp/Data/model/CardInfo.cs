using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data.model
{
    public class CardInfo
    {
        public string CardNumber { get; set; }
        public string ValidThrough { get; set; }
        public string Cvv { get; set; }

        public string Name { get; set; }
    }
}
