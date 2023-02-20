using IdentityServer.Models;

namespace IdentityServer.Services
{
    public interface IFileService
    {
        string SaveFile(IFormFile  formFile,IConfiguration configuration);
        AppResult DeleteFile(string fileName, IConfiguration configuration);
    }
}
