using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace RMS.Models.Item
{
    public class AddViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; }
        public AddViewModel()
        {
            Paths = new RoutePath[]
            {
               new RoutePath { ControllerName = "Home", ActionName = "Index", Titile = "Home" },
               new RoutePath { ControllerName = "Item", ActionName = "Index",Titile = "Food"},
               new RoutePath { ControllerName = "Item", ActionName = "Add",Titile = "Add"}
            };
        }
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int? Discount { get; set; }
        [MaxLength(60)]
        public string? Logo { get; set; }
        // [Required]
        // public MenuCategory? Category { get; set; }
        public List<SelectListItem>? Categories { get; set; }

        public int CategoryId { get; set; }
        public IFormFile? UploadLogo { get; set; }

    }
}
