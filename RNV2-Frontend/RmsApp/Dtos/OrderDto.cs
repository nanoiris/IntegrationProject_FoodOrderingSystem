using System;
using System.ComponentModel.DataAnnotations;
using RestaurantDaoBase.Models;
using RestaurantDaoBase.Enums;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;

namespace RmsApp.Dtos
{
    public class OrderDto
    {
        public string? Id { get; set; }
        public string? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime? DesiredTime { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? PayTime { get; set; }
        public PayCard? Card { get; set; }


        public decimal? PayTotal { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? DeliveryFee { get; set; }

        public decimal? Tax { get; set; }

        public decimal? Tips { get; set; }

        public bool IsDelivery { get; set; }
        public List<OrderItem>? ItemList { get; set; }

    }

}