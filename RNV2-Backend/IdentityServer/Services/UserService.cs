using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IdentityServer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
            if (roles != null && roles.Count >= 1)
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
                RestaurantId = model.RestaurantId,
                RestaurantName = model.RestaurantName
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                return await addRole(user, model.Role);
            var appResult = new AppResult("User is not created", false);
            appResult.Errors = result.Errors.Select(e => e.Description);
            return appResult;
        }

        public async Task<AppResult> DeleteUserByEmail(string email)
        {
            AppUser user = await FindUserByEmail(email);
            if (user == null)
            {
                return new AppResult("No the user(userId ={id}", false);
            }
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                AppResult appResult = new AppResult("Cannot delete the user(userId ={id}", false);
                appResult.Errors = result.Errors.Select(x => x.Description);
                return appResult;
            }
            return new AppResult("", true);
        }
        public Task<AppUser>? FindUserById(string id)
        {
            return userManager.FindByIdAsync(id);
        }
        public Task<AppUser>? FindUserByEmail(string email)
        {
            return userManager.FindByEmailAsync(email);
        }

        public async Task<AppResult> UpdateUser(UpdateUserViewModel model)
        {
            if (model.Email == null)
                return new AppResult("Cannot update the user because the email is null", false);
            AppUser user = await FindUserByEmail(model.Email);
            if (user == null)
                return new AppResult($"Cannot update the user because the user({model.Email}) is null", false);

            user.Name = model.Name;
            user.Logo = model.Logo;
            user.RestaurantId = model.RestaurantId;
            user.RestaurantName = model.RestaurantName;
            if(model.Role != null) 
            {
                user.Role = model.Role;
            }

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                AppResult appResult = new AppResult($"Cannot update the user({model.Email})", false);
                appResult.Errors = result.Errors.Select(x => x.Description);
                return appResult;
            }
            return new AppResult("Update the user successfully", true);
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

        public async Task<AppResult> ResetPassword(string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return new AppResult("Confirm password is not the same as the password!", false);

            AppUser user = await FindUserByEmail(email);
            if (user == null)
                return new AppResult($"The user({email} does not exist", false);
            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, code, password);
            if (!result.Succeeded)
            {
                AppResult appResult = new AppResult("Cannot reset the password", false);
                appResult.Errors = result.Errors.Select(x => x.Description);
                return appResult;
            }
            return new AppResult("", true);
        }

        public Task Logout()
        {
             return signInManager.SignOutAsync();
        }

        public Task<List<AppUser>> ListRestaurantUsers(string restaurantId)
        {
            return userManager.Users.Where(x => x.RestaurantId == restaurantId).ToListAsync();
        }
    }
}
