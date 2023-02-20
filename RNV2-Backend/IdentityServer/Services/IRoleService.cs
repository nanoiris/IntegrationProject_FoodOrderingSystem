using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public interface IRoleService
    {
        public List<IdentityRole> ListRole();
        public void AddRole(string name);
        public void DeleteRole(string id);
    }
}
