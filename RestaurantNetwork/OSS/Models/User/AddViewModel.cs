using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RestaurantDao.Models;
using RestaurantDao.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OSS.Models.User
{
    public class AddViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[] 
        {
            new RoutePath("Home", "Index", "Home"),
            new RoutePath("User", "Index","User"),
            new RoutePath("User", "Add","Add"),
        };
        
        public int? RestaurantId { get; set;}
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? logo { get; set; }
        public IFormFile? UploadLogo { get; set; }
    }
}
