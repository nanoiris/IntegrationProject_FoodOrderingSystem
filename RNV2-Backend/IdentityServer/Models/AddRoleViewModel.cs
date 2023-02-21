using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class AddRoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
