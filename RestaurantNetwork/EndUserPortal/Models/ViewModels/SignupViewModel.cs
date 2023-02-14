
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EndUserPortal.Models.ViewModels
{
    public class SignupViewModel

    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]{2,20}$", ErrorMessage = "Username should be 2-30 digit and alphabet.")]
        public string Name { get; set; }
        [RegularExpression("^[0-9+-, ]{10,20}$", ErrorMessage = "Only digit, + - and space allowed(10-20).")]
        public string PhoneNo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public IFormFile? Upload { get; set; }

        public string? Message { get; set; }

    }
}
