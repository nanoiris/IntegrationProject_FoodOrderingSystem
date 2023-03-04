using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssApp.Model
{
    public class AppUserForm : LogoModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Logo { get; set; }
        public string? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public int? Status { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
