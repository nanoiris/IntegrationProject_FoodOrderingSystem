using RestaurantDaoBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data.Dto
{
    public class OrderItemDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("item")]
        public MenuItemDto? Item { get; set; }
        [JsonPropertyName("qty")]
        public int Qty { get; set; }
        [JsonPropertyName("status")]
        public StatusEnum Status { get; set; }
        [JsonPropertyName("orderId")]
        public string? OrderId { get; set; }
    }
}
