using IdentityServer.Models;

namespace IdentityServer.Services
{
    public interface IFileService
    {
        string SaveFile(IFormFile  formFile);
        AppResult DeleteFile(string fileName);
    }
}
