using RestaurantDao.Models;
using RestaurantDao.ViewModel;

namespace RestaurantDao.IServices
{
    public interface IAuthService
    {
        public AppUser Login(string username, string password);
        public void Signup(UserViewModel userViewModel);
        public void Logout();
        public void ChangePassword(string email, string password, string retypePassword);
        public void ChangeProfile(UserViewModel userViewModel);
        public void AddUser(AppUser user, Stream logo);
        public void UpdateUser(AppUser user, Stream logo);
        public AppUser FindAppUserById(int id);

    }
}
