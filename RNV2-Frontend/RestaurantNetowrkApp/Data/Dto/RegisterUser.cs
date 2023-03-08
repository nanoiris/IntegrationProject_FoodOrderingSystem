using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data.Dto
{
    public class RegisterUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]{2,20}$", ErrorMessage = "Username should be 2-30 digit and alphabet.")]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [RegularExpression("^[0-9+-, ]{10,20}$", ErrorMessage = "Only digit, + - and space allowed(10-20).")]
        public string? PhoneNumber { get; set; }
        public IBrowserFile? UploadImg { get; set; }
        public string? Logo { get; set; }
        public string? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        [MaxLength(20)]
        public string? District { get; set; }
        [MaxLength(30)]
        public string? Street { get; set; }
        [MaxLength(30)]
        public string? City { get; set; }
        [MaxLength(30)]
        public string? State { get; set; }
        [MaxLength(30)]
        public string? Country { get; set; }
        [MaxLength(20)]
        public string? PostCode { get; set; }
        public string? Role { get; set; }
    }
}
