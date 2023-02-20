using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Models
{
    public class AppUser : IdentityUser
    {
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(60)]
        public string? Logo { get; set; }
       
        public int? RestaurantId { get; set; }
        [MaxLength(40)]
        public string? RestaurantName { get; set; }
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
        public string? ZipCode { get; set; }

        [NotMapped]
        public string? Role { get; set; }
    }
}
