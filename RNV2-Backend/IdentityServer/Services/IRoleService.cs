using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public interface IRoleService
    {
        public List<IdentityRole> ListRole();
        public IdentityRole AddRole(string name);
        public AppResult UpdateRole(string id,string name);
        public AppResult DeleteRole(string id);
        public IdentityRole FindById(string id);
    }
}
