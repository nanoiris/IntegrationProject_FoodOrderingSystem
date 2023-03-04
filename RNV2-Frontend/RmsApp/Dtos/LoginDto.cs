using System;
using System.ComponentModel.DataAnnotations;
namespace RmsApp.Dtos
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
        public string Logo { get; set; }
        public string Token { get; set; }
        public string RestaurantId { get; set; }
        public string RestaurantName { get; set; }

    }
}