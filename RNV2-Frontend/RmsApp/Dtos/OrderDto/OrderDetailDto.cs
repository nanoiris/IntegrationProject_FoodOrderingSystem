using System;
using System.ComponentModel.DataAnnotations;
namespace RmsApp.Dtos
{
    public class OrderDetailDto
    {
        public decimal? PayTotal { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? DeliveryFee { get; set; }

        public decimal? Tax { get; set; }

        public decimal? Tips { get; set; }

        public bool IsDelivery { get; set; }
        // public List<OrderItem> ItemList { get; set; }

    }
}