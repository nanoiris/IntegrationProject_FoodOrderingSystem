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
