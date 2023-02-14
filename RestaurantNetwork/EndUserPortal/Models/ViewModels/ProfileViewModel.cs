using System.ComponentModel.DataAnnotations;

namespace EndUserPortal.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string? Message { get; set; }
        public int? RestaurantId { get; set; }
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]{2,20}$", ErrorMessage = "Username should be 2-30 digit and alphabet.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Password { get; set; }

        [RegularExpression("^[0-9+-, ]{10,20}$", ErrorMessage = "Only digit, + - and space allowed(10-20).")]
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public string? idUserId { get; set; }
        public IFormFile? UploadLogo { get; set; }

    }
}
