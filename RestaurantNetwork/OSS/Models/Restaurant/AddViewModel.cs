﻿using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RestaurantDao.Models;
using RestaurantDao.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OSS.Models.Restaurant
{
    public class AddViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; } = new RoutePath[] 
        {
            new RoutePath("Home", "Index", "Home"),
            new RoutePath("Restaurant", "Index","Restaurant"),
            new RoutePath("Restaurant", "Add","Add"),
        };
        public string Name { get; set; }
        
        [MaxLength(100)]
        public string? Description { get; set; }
        [MaxLength(60)]
        public string? Logo { get; set; }
        [MaxLength(80)]
        public string? Street { get; set; }
        [MaxLength(40)]
        public string? City { get; set; }
        [MaxLength(40)]
        public string? State { get; set; }
        [MaxLength(10)]
        public string? Country { get; set; }
        [MaxLength(16)]
        public string? Zip { get; set; }
        [MaxLength(16)]
        public string? PhoneNo { get; set; }
        [MaxLength(40)]
        public string? Email { get; set; }
        public IFormFile? UploadLogo { get; set; }
        public List<SelectListItem>? Categories { get; set; }

        public int? CategoryId { get; set; }
        public bool IsFeatured { get; set; }
        
       
    }
}
