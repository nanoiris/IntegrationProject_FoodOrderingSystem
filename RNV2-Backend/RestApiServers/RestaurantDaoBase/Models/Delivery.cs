using RestaurantDaoBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class Delivery
    {
        public string? Id { get; set; }
        public string OrderId { get; set; }
        public RestAddress? Restaurant { get; set; }
        public string? CustomerName { get; set; }
        public Address? Destination { get; set; }
        public string? DeliveryMan { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? AcceptTime { get; set; }
        public DateTime? PickupTime { get; set; }
        public DateTime? EstimateTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public DeliveryStatusEnum? Status { get; set; }

    }
}
