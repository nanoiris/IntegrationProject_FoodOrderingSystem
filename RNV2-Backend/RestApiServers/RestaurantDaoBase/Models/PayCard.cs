using RestaurantDaoBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class PayCard
    {
        public PayTypeEnum? CardType { get; set; } 
        public string? CardName { get; set; }
        public string? CardNo { get; set; }
        public string? Cvv { get; set; }
        public DateTime? ValidTime { get; set; }
    }
}
