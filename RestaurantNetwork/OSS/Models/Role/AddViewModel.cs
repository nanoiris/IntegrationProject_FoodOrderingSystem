using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RestaurantDao.Models;
using RestaurantDao.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OSS.Models.Role
{
    public class AddViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[] 
        {
            new RoutePath("Home", "Index", "Home"),
            new RoutePath("Role", "Index","Role"),
            new RoutePath("Role", "Add","Add"),
        };
        public string? Name { get; set; }
        
    }
}
