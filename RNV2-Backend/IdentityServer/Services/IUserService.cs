using IdentityServer.Models;

namespace IdentityServer.Services
{
    public interface IUserService
    {
        Task<AppResult> RegisterUser(RegisterViewModel model);
        Task<AppUser> Login(LoginViewModel model);
    }
}
