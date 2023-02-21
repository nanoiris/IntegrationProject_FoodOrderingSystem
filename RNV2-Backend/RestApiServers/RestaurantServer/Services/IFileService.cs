using RestaurantDaoBase.Models;

namespace RestaurantServer.Services
{
    public interface IFileService
    {
        string SaveFile(IFormFile  formFile);
        AppResult DeleteFile(string fileName);
    }
}
