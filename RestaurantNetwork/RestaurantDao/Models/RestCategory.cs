using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Models
{
    public class RestCategory
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string? Name { get; set; }
        [MaxLength(60)]
        public string? Logo { get; set; }
        [NotMapped]
        public string? FullLogoPath => AppDbContext.GetBlobUrlPrefix() + Logo; 
        public List<Restaurant>? Restaurants { get; set; }
    }
}
