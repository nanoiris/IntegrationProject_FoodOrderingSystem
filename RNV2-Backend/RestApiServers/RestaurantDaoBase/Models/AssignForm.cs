using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class AssignForm
    {
        public string Id { get; set; }
        public string DeliveryMan { get; set; }
        public DateTime EstimateTime { get; set; }
        public string? CreateBy { get; set; }
        public string? CreateTime { get; set; }
    }
}
