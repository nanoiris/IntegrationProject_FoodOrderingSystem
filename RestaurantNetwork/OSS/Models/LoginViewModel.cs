using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RestaurantDao.Models;
using RestaurantDao.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OSS.Models
{
    public class LoginViewModel
    {
        
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
