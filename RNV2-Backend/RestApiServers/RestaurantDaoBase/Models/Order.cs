using RestaurantDaoBase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class Order
    {
        public string? Id { get; set; }
        public string? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public StatusEnum Status {get; set; }
        public DateTime? DesiredTime { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? PayTime{ get; set; }
        public PayCard? Card { get; set; }
        
        [Column(TypeName = "money")]
        public decimal? PayTotal { get; set; }
        [Column(TypeName = "money")]
        public decimal? SubTotal { get; set; }
        [Column(TypeName = "money")]
        public decimal? DeliveryFee { get; set; }
        [Column(TypeName = "money")]
        public decimal? Tax { get; set; }
        [Column(TypeName = "money")]
        public decimal? Tips { get; set; }

        public bool IsDelivery { get; set; }
        public List<OrderItem>? ItemList { get; set; }

    }
}
