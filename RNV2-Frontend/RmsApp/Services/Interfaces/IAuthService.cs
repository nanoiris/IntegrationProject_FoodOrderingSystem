using System.Collections.Generic;
using System.Threading.Tasks;
using RmsApp.Dtos;

namespace RmsApp.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
        void Logout();
        // bool IsLoggedIn { get; set; }
    }
}