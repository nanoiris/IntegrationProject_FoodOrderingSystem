﻿using OssApp.Model;
using System.Net.Http.Json;
using Serilog;

namespace OssApp.Services
{
    public class AuthService
    {
        private HttpClient http;
        private string identityServer;
        public static string authUrl = "api/Auth";
        public static string loginUrl = $"{authUrl}/Login";
        public static UserModel User { get; set; } 
        public static bool IsLoggedIn { get; set; }
        
        public AuthService(string identityServer)
        {
            this.identityServer = identityServer;
            http = new HttpClient();
            http.BaseAddress = new Uri(identityServer);
            IsLoggedIn = true;

            User = new UserModel();
            User.UserName = "k@a.com";
            User.Logo = null;
        }
       
        public async Task<bool> Login(string username, string password)
        {
            var response = await http.PostAsJsonAsync(loginUrl, new { UserName = username,Password = password });
            if (response.IsSuccessStatusCode)
            {
                Log.Debug("Login succefully");
                User = await response.Content.ReadFromJsonAsync<UserModel>();
                IsLoggedIn = true;
                if(User.Logo != null)
                {
                    User.Logo = Utils.BuildLogoPath(User.Logo);
                }
                return true;
            }
            Log.Debug("Login failed,login again");
            return false;
        }

        public void Logout()
        {
            IsLoggedIn= false;
        }
    }
}