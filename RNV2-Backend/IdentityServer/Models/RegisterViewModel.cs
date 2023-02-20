using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? UploadImg { get; set; }
        public string? Logo { get; set; }
        public int? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public string? Role { get; set; }
    }
}
