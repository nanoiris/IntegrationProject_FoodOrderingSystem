using Microsoft.Build.Framework;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace OSS.Models.User
{
    public class DeleteViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[] 
        {
            new RoutePath("Home", "Index", "Home"),
            new RoutePath("User", "Index","User"),
            new RoutePath("User", "Delete","Delete")
        };
        public RestaurantDao.Models.AppUser DeleteRow { get; set; }   
    }
}
