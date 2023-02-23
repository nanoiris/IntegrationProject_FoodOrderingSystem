using RestaurantDaoBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class RestAndStatusViewModel
    {
        public string RestaurantId { get; set; }
        public StatusEnum Status { get; set; }
    }
}
