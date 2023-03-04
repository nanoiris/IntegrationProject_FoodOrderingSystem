using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class UserAddressViewModel
    {
        public string Email { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostCode { get; set; }
    }
}
