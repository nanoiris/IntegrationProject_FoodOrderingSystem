using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class AppUser
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Logo { get; set; }
        public int? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public string? Role { get; set; }
        public Address? Address { get; set;}
    }
}
