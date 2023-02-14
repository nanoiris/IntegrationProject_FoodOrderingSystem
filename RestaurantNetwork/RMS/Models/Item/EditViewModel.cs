using Microsoft.Build.Framework;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace RMS.Models.Item
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
               new RoutePath { ControllerName = "Item", ActionName = "Index",Titile = "Food"},
               new RoutePath { ControllerName = "Item", ActionName = "Edit",Titile = "Edit"}
            };
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int? Discount { get; set; }
        [MaxLength(60)]
        public string? Logo { get; set; }
        public string? FullLogoPath { get; set; }

        public List<SelectListItem>? Categories { get; set; }

        public int CategoryId { get; set; }
        public IFormFile? UploadLogo { get; set; }

        public bool IsFeatured { get; set; }
    }
}
