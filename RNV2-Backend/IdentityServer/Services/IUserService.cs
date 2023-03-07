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
        Task<AppResult> UpdateUserStatus(string email,UserStatusEnum status);
        Task<AppResult> AddUser(AddUserViewModel model);

        Task<AppResult> UpdateUserMain(AddUserViewModel model);
        Task<AppResult> UpdateUserAddress(UserAddressViewModel model);
        Task<AppResult> UpdateUserLogo(string email,string logoPath);

        List<object>? AvaliableDeliveryManList();
    }
}
