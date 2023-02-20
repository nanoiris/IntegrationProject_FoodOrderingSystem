using RestaurantDao.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Models
{
    public class Delivery
    {
        public string Id { get; set; }
        public Restaurant Provider { get; set; }
        public string CustomerName { get; set; }
        public Address Destination { get; set; }   
        public string DeliveryMan { get; set; }
        public DateTime AcceptTime { get; set; }
        public DateTime EstimateTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DeliveryStatusEnum Status { get; set; }
        
    }
}
