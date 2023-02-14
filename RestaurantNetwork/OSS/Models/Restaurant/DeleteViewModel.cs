using Microsoft.Build.Framework;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace OSS.Models.Restaurant
{
    public class DeleteViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[] 
        {
            new RoutePath("Home", "Index", "Home" ),
            new RoutePath("Restaurant", "Index","Restaurant"),
            new RoutePath("Restaurant", "Delete","Delete")
        };
        public RestaurantDao.Models.Restaurant DeleteRest { get; set; }   
    }
}
