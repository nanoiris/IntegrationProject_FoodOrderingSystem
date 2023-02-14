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
    public class OrderItem
    {
        public int Id { get; set; }
        public MenuItem Item { get; set; }
        public int Qty { get; set; }
        [NotMapped]
        public string SubTotal => (Item.Price * Qty).ToString("0.00");
        public StatusEnum Status { get; set; }
        public Order order { get; set; }
    }
}
