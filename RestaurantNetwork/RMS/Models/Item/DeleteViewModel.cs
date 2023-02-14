using Microsoft.Build.Framework;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace RMS.Models.Item
{
    public class DeleteViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; }
        public DeleteViewModel()
        {
            Paths = new RoutePath[]
            {
               new RoutePath { ControllerName = "Home", ActionName = "Index", Titile = "Home" },
               new RoutePath { ControllerName = "Item", ActionName = "Index",Titile = "Food"},
               new RoutePath { ControllerName = "Item", ActionName = "Delete",Titile = "Delete"}
            };
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
