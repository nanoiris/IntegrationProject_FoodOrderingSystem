using Microsoft.Build.Framework;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace OSS.Models.Order
{
    public class DeleteViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[]
        {
           new RoutePath("Home", "Index", "Home"),
           new RoutePath("Order", "Index", "Order"),
           new RoutePath("Order", "Delete","Delete")
        };
        public RestaurantDao.Models.Order DeleteRow { get; set; }
    }
}
