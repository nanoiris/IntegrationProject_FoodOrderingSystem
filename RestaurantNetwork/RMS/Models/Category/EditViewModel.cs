using Microsoft.Build.Framework;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace RMS.Models.Category
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
               new RoutePath { ControllerName = "Category", ActionName = "Index",Titile = "Category"},
               new RoutePath { ControllerName = "Category", ActionName = "Edit",Titile = "Edit"}
            };
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
