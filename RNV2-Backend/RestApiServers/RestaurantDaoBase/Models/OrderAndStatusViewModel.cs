using RestaurantDaoBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class OrderAndStatusViewModel
    {
        public string OrderId { get; set; }
        public StatusEnum Status { get; set; }
    }
}
