using DeliveryApp.Services;
using RestaurantDaoBase.Models;
using Serilog;
using System.Net.Http.Json;

namespace DeliveryApp.Services
{
    public class DeliService<T>
    {
        internal HttpClient http;
        public string server;
        
        public DeliService(string server)
        {
            this.server = server;
            http = new HttpClient();
            http.BaseAddress = new Uri(server);

            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
        }

    }
}
