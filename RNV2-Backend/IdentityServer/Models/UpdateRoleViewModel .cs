using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class UpdateRoleViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
