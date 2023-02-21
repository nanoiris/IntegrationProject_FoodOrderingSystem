using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public string? Role { get; set; }
    }
}
