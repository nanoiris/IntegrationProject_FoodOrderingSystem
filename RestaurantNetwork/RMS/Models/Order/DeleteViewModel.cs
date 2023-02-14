using Microsoft.Build.Framework;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace RMS.Models.Order
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
               new RoutePath { ControllerName = "Order", ActionName = "Index",Titile = "Order"},
               new RoutePath { ControllerName = "Order", ActionName = "Delete",Titile = "Delete"}
            };
        }

        public RestaurantDao.Models.Order DeleteRow { get; set; }
    }
}
