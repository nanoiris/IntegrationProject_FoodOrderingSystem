using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data.Dto
{
    public class RestaurantDto
    {
        public string? Id { get; set; }
        //public string? PartionKey { get; set; }
        public string Name { get; set; }
        public bool IsFeatured { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public Address Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string CategoryId { get; set; }
    }
}
