using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantDaoBase.Models;

namespace RmsApp.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant?> GetOneRestaurantAsync(string id);
        Task<Restaurant?> UpdateRestaurantAsync(RestaurantForm form);

    }
}