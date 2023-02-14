using Microsoft.Build.Framework;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace OSS.Models.Order
{
    public class DetailViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[]
        {
            new RoutePath("Home", "Index", "Home"),
            new RoutePath("Order", "Index","Order"),
            new RoutePath("Order", "Detail","Detail")
        };
        public string[] TabHeaders { get; set; } = new string[] { "Image", "ID", "Name", "Price", "QTY", "Subtotal" };
        
        public RestaurantDao.Models.Order Row { get; set; }
    }
}
