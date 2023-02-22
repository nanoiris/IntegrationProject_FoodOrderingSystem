using Microsoft.AspNetCore.Http;
using RestaurantDaoBase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class MenuItem
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsFeatured { get; set; }
        public decimal Price { get; set; }
        public string? Logo { get; set; }
        public string? CategoryId { get; set; }

        [NotMapped]
        public IFormFile? UploadImg { get; set; }
    }
}
