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
    public class MenuItem
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        public FeaturedEnum? Featured { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string PriceStr => Price.ToString("0.00");
        public int? Discount { get; set; }
        [MaxLength(60)]
        public string? Logo { get; set; }
        public MenuCategory? Category { get; set; }
        public Restaurant Owner { get; set; }

        [NotMapped]
        public string? FullLogoPath => AppDbContext.GetBlobUrlPrefix() + Logo;
    }
}
