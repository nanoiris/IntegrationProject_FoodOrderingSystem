using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data.Dto
{
    public class MenuItemDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("isFeatured")]
        public bool IsFeatured { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("logo")]
        public string? Logo { get; set; }
        [JsonPropertyName("categoryId")]
        public string? CategoryId { get; set; }

    }
}
