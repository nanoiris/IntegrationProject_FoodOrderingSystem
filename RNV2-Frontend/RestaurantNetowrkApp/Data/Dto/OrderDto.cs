using RestaurantDaoBase.Enums;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data.Dto
{
    public class OrderDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("restaurantId")]
        public string? RestaurantId { get; set; }
        [JsonPropertyName("restaurantName")]
        public string? RestaurantName { get; set; }
        [JsonPropertyName("status")]
        public StatusEnum Status { get; set; }
        [JsonPropertyName("desiredTime")]
        public DateTime? DesiredTime { get; set; }
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        [JsonPropertyName("payTime")]
        public DateTime? PayTime { get; set; }
        [JsonPropertyName("card")]
        public PayCard? Card { get; set; }
        [JsonPropertyName("payTotal")]
        [Column(TypeName = "money")]
        public decimal? PayTotal { get; set; }
        [JsonPropertyName("subTotal")]
        [Column(TypeName = "money")]
        public decimal? SubTotal { get; set; }
        [JsonPropertyName("deliveryFee")]
        [Column(TypeName = "money")]
        public decimal? DeliveryFee { get; set; }
        [JsonPropertyName("tax")]
        [Column(TypeName = "money")]
        public decimal? Tax { get; set; }
        [JsonPropertyName("tips")]
        [Column(TypeName = "money")]
        public decimal? Tips { get; set; }
        [JsonPropertyName("isDelivery")]

        public bool IsDelivery { get; set; }
        [JsonPropertyName("itemList")]
        public List<OrderItemDto>? ItemList { get; set; }
    }
}
