using Microsoft.Build.Framework;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace RMS.Models.Order
{
    public class DetailViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; }
        public string[] TabHeaders { get; set; }
        public DetailViewModel()
        {
            Paths = new RoutePath[]
            {
               new RoutePath { ControllerName = "Home", ActionName = "Index", Titile = "Home" },
               new RoutePath { ControllerName = "Order", ActionName = "Index",Titile = "Order"},
               new RoutePath { ControllerName = "Order", ActionName = "Detail",Titile = "Detail"}
            };
            TabHeaders = new string[] { "Image","ID", "Name", "Price", "QTY", "Subtotal" };
        }

        public RestaurantDao.Models.Order Row { get; set; }
      
    }
}
