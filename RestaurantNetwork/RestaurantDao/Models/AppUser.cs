using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        [Required,MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(16)]
        public string? PhoneNo { get; set; }
        [MaxLength(40)]
        public string Email { get; set; }

        [NotMapped]
        public IdentityUser IdUser { get; set; }
        [MaxLength(60)]
        public string? Logo { get; set; }
        [NotMapped,MaxLength(60)]
        public string Avatar => AppDbContext.GetBlobUrlPrefix() + Logo;
        public Restaurant? Restaurant { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string? Role { get; set; }
    }
}
