using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Services
{
    public class RoleService : IRoleService
    {
        public readonly RoleManager<IdentityRole> roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public AppResult AddRole(string name)
        {
            AppResult appResult = new AppResult($"Add the new role(name={name} successfully",true);

            IdentityRole role = new IdentityRole
            {
                Name = name
            };

            var result = roleManager.CreateAsync(role)
                .GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                appResult = new AppResult("OssService.AddRole : cannot add the new role", false);
                appResult.Errors = result.Errors.Select(x => x.Description);
            }
            return appResult;
        }

        public AppResult DeleteRole(string id)
        {
            var role = roleManager.FindByIdAsync(id).GetAwaiter().GetResult();
            if(role == null)
            {
                return new AppResult("OssService.DeleteRole : the role(id = {id} does not exist", false);
            }
            var result = roleManager.DeleteAsync(role).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                var appResult = new AppResult("OssService.AddRole : cannot add the new role", false);
                appResult.Errors = result.Errors.Select(x => x.Description);
                return appResult;
            }
            return new AppResult($"Delete the role(id={id}) successfully", true);
        }

        public List<IdentityRole> ListRole()
        {
            return roleManager.Roles.ToList();
        }

        public IdentityRole? FindById(string id)
        {
            return roleManager.FindByIdAsync(id)
                .GetAwaiter().GetResult();
        }
    }
}
