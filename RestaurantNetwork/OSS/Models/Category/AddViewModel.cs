using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RestaurantDao.Models;
using RestaurantDao.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OSS.Models.Category
{
    public class AddViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[]
        {
            new RoutePath("Home","Index","Home"),
            new RoutePath("Category", "Index","Category"),
            new RoutePath("Category", "Add","Add"),
        };
        
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public IFormFile? UploadLogo { get; set; }
    }
}
