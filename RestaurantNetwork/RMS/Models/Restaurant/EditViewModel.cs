using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace RMS.Models.Restaurant
{
    public class EditViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; }
        public EditViewModel()
        {
            Paths = new RoutePath[]
            {
               new RoutePath { ControllerName = "Home", ActionName = "Index", Titile = "Home" },
               new RoutePath { ControllerName = "Restaurant", ActionName = "Edit",Titile = "Restaurant"}
            };
        }
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }

        public IFormFile? UploadLogo { get; set; }
        public string? FullLogoPath { get; set; }
        [MaxLength(60)]
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
        public List<SelectListItem>? Categories { get; set; }
        public int? CategoryId { get; set; }

    }
}
