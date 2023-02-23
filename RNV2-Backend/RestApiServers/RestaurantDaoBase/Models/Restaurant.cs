using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class Restaurant
    {
        public string? Id { get; set; }
        //public string? PartionKey { get; set; }
        public string Name { get; set; }
        public bool? IsFeatured { get; set; }
        public string? Description { get; set; }
        public string? Logo { get; set; }
        public Address? Address { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string CategoryId { get; set; }
    }
}
