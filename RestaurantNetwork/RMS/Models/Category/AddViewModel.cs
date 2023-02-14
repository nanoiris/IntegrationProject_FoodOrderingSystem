using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace RMS.Models.Category
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
               new RoutePath { ControllerName = "Category", ActionName = "Index",Titile = "Category"},
               new RoutePath { ControllerName = "Category", ActionName = "Add",Titile = "Add"}
            };
        }
        [Required, MaxLength(20)]
        public string Name { get; set; }
    }
}
