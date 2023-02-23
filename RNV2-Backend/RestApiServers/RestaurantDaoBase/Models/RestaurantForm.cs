using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class RestaurantForm
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Logo { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public bool? IsFeatured { get; set; }
        public string? CategoryId { get; set; }
        //public string? PartionKey { get; set; }
        
        [NotMapped]
        public IFormFile? UploadImg { get; set; }
    }
}
