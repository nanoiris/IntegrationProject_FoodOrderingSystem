using RestaurantDaoBase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class RestRating
    {
        public string? Id { get; set; }
        //public string? PartionKey { get; set; }
        public string RestaurantId { get; set; }
        public int Value { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class MenuItemRating
    {
        public string? Id { get; set; }
        //public string? PartionKey { get; set; }
        public string RestaurantId { get; set; }
        public string MenuItemId { get; set; }
        public int Value { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class DeliveryRating
    {
        public string? Id { get; set; }
        //public string? PartionKey { get; set; }
        public string OrderId { get; set; }
        public string DeliveryManId { get; set; }
        public int Value { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
