using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using RestaurantNetowrkApp.Data.model;

namespace RestaurantNetowrkApp.Services
{
    public class UserService
    {
        internal HttpClient http;
        public string server;
        
        public UserService(string server)
        {
            this.server = server;
            http = new HttpClient();
            http.BaseAddress = new Uri(server);
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
        }
        public UserModel getUserByEmail(string userEmail)
        {
            return http.GetFromJsonAsync<UserModel>($"api/user/OneByEmail/{userEmail}").Result;
        }
    }
}
