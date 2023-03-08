using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data.Dto
{
    public class RestaurantDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        //public string? PartionKey { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isFeatured")]
        public bool IsFeatured { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("logo")]
        public string Logo { get; set; }
        [JsonPropertyName("address")]
        public Address Address { get; set; }
        [JsonPropertyName("phoneNo")]
        public string PhoneNo { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("categoryId")]
        public string CategoryId { get; set; }
    }
}
