using System.Collections.Generic;
using System.Threading.Tasks;
using RmsApp.Dtos;

namespace RmsApp.Services
{
    public interface IRestaurantService
    {
        Task<RestaurantDto?> GetOneRestaurantAsync(string id);
        Task UpdateRestaurantAsync(RestaurantDto restaurant);

    }
}