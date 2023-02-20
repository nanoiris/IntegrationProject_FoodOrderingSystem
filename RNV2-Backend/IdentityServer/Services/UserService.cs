using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace IdentityServer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserService(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<AppUser?> Login(LoginViewModel model)
        {
            AppUser appUser = await userManager.FindByEmailAsync(model.UserName);
            if (appUser == null)
                return null;
            
            var result = await userManager.CheckPasswordAsync(appUser, model.Password);
            if (!result == true)
                return null;

            var roles = await userManager.GetRolesAsync(appUser);
           if(roles != null && roles.Count >= 1)
            {
                appUser.Role = roles[0];
            }
            return appUser;
        }

        public async Task<AppResult> RegisterUser(RegisterViewModel model)
        {
            if (model == null)
                return new AppResult("register model is null", false);

            if (model.Password != model.ConfirmPassword)
                return new AppResult("Confirm password is not the same as the password!", false);
            var user = new AppUser
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Email,
                Logo = model.Logo,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                return await addRole(user, model.Role);
            var appResult = new AppResult("User is not created", false);
            appResult.Errors = result.Errors.Select(e => e.Description);
            return appResult;
        }
        private async Task<AppResult> addRole(AppUser user, string role)
        {
            if (role == null)
                return new AppResult("No role to add to the user, the user created successfully!", true);
            
            try
            {
                var result = await userManager.AddToRoleAsync(user, role);
                if (!result.Succeeded)
                {
                    var appResult = new AppResult("User is not created during the role registration", false);
                    appResult.Errors = result.Errors.Select(e => e.Description);
                    return appResult;
                }
            }
            catch (Exception ex)
            {
                await userManager.DeleteAsync(user);
                var appResult = new AppResult("User is not created during the role registration", false);
                appResult.Errors = new string[] { ex.Message };
                return appResult;
            }
            return new AppResult("User is created successfully", true);
        }
    }
}
