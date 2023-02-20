using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public interface IRestaurantUserService
    {
        public void AddRestaurantUser(AppUser user, Stream logo);
        public List<AppUser> ListRestaurantUsers(int restaurantId);
        public AppUser? FindRestaurantUser(int id);
        public void DeleteRestaurantUser(int id);
        public void UpdateRestaurantUser(AppUser user, Stream logo);
    }
}
