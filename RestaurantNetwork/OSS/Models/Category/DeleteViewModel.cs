using Microsoft.Build.Framework;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace OSS.Models.Category
{
    public class DeleteViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[]
        {
            new RoutePath("Home","Index","Home"),
            new RoutePath("Category", "Index","Category"),
            new RoutePath("Category", "Delete","Delete")
        };
        public RestCategory DeleteRow { get; set; }   
    }
}
