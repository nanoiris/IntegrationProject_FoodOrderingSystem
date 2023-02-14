using RestaurantDao.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Restaurant Provider { get; set; }
        public StatusEnum Status {get; set; }
        [MaxLength(30)]
        public string UserName { get; set; }
        public DateTime? DesiredTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? PayTime{ get; set; }
        public PayTypeEnum? PayType { get; set; }
        [MaxLength(20)]
        public string? PayCard { get; set; }
        [MaxLength(40)]
        public string? PayName { get; set; }
        [Column(TypeName = "money")]
        public decimal? PayTotal { get; set; }
        [Column(TypeName = "money")]
        public decimal? SubTotal { get; set; }

        [NotMapped]
        public string? ItemTotal => "$" + SubTotal?.ToString("0.00");
        [NotMapped]
        public string? GSTHST => "$" + (SubTotal * 0.1m)?.ToString("0.00");
        [NotMapped]
        public string? PSTQST => "$" + (SubTotal * 0.05m)?.ToString("0.00");
        [NotMapped]
        public string? OrderTotal => "$" + (SubTotal * 1.15m)?.ToString("0.00");

        public List<OrderItem> OrderItems { get; set; }
    }
}
