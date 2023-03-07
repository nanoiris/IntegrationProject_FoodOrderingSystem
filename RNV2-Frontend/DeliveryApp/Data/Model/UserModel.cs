using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Data.Model
{
    public class UserModel
    {
        public string UserName { get; set; }

        public string Name { get; set; }
        public string Role { get; set; }
        public string Logo { get; set; }
        public string Token { get; set; }
        public string RestaurantId { get; set; }
        public string RestaurantName { get; set; }

        [MaxLength(20)]
        public string? District { get; set; }
        [MaxLength(30)]
        public string? Street { get; set; }
        [MaxLength(30)]
        public string? City { get; set; }
        [MaxLength(30)]
        public string? State { get; set; }
        [MaxLength(30)]
        public string? Country { get; set; }
        [MaxLength(20)]
        public string? PostCode { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public IBrowserFile? UploadImg { get; set; }
    }
}
