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
    public class OrderItem
    {
        public string? Id { get; set; }
        public MenuItem? Item { get; set; }
        public int Qty { get; set; }
        public StatusEnum Status { get; set; }
        public string? OrderId { get; set; }
    }
}
