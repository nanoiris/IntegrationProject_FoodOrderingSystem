
using IdentityServer.Models;

namespace IdentityServer.Services
{
    public interface ITokenService
    {
        public string GenerateToken(AppUser user,IConfiguration configuration);
    }
}
