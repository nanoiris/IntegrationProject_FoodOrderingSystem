using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace OSS.Models.User
{
    public class EditViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[] 
        {
          new RoutePath("Home", "Index", "Home"),
          new RoutePath("User", "Index","User"),
          new RoutePath("User", "Edit","Edit")
        };
        public int? RestaurantId { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public string? idUserId { get; set; }
        public IFormFile? UploadLogo { get; set; }
    }
}
