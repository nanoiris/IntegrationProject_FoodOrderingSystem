using IdentityServer.Models;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public partial class UserService : IUserService
    {
        public async Task<AppResult> UpdateUserStatus(string email, UserStatusEnum status)
        {
            AppUser user = await FindUserByEmail(email);
            if (user == null)
            {
                return new AppResult($"No such a user email={email}",false);
            }

            user.Status = status;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new AppResult("", true);
            }
            AppResult appResult = new AppResult("Failed to update user status", false);
            appResult.Errors = result.Errors.Select(x => x.Description);
            return appResult;
        }
    }
}
