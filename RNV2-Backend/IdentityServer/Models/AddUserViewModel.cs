using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class AddUserViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public UserStatusEnum Status { get; set; }
    }
}
