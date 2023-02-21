using IdentityServer.Models;

namespace IdentityServer.Services
{
    public interface IUserService
    {
        Task<AppResult> RegisterUser(RegisterViewModel model);
        Task<AppUser> Login(LoginViewModel model);
        Task Logout();
        Task<AppUser>? FindUserById(string id);
        Task<AppUser>? FindUserByEmail(string email);
        Task<AppResult> DeleteUserByEmail(string email);
        Task<AppResult> UpdateUser(UpdateUserViewModel model);
        Task<AppResult> ResetPassword(string email,string password,string confirmPassword);
        Task<List<AppUser>>? ListRestaurantUsers(string restaurantId);
    }
}
