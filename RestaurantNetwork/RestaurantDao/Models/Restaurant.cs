using RestaurantDao.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        [MaxLength(60)]
        public string? Logo { get; set; }
        [MaxLength(80)]
        public string? Street { get; set; }
        [MaxLength(40)]
        public string? City { get; set; }
        [MaxLength(40)]
        public string? State { get; set; }
        [MaxLength(10)]
        public string? Country { get; set; }
        [MaxLength(16)]
        public string? Zip { get; set; }
        [MaxLength(16)]
        public string? PhoneNo { get; set; }
        [MaxLength(40)]
        public string? Email { get; set; }
        public FeaturedEnum? Featured { get; set; }
        public int? Rating { get; set; }

        //public int? CategoryId { get; set; }
        public RestCategory Category { get; set; }
        public List<MenuCategory>? MenuCategories { get; set; }
        public List<MenuItem>? Menus { get; set; }
        public List<Rating> Ratings { get; set; }

        [NotMapped]
        public string FullLogoPath => AppDbContext.GetBlobUrlPrefix() + Logo;     
    }
}
