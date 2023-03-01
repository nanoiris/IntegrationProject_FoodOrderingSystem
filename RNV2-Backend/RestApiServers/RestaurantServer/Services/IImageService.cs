using RestaurantDaoBase.Models;

namespace RestaurantServer.Services
{
    public interface IFileService<T>
    {
        string SaveFile(T  formFile);
        AppResult DeleteFile(string fileName);
    }
}
