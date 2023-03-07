using System.Net.Http.Json;
using Serilog;
using Microsoft.AspNetCore.Components;
using DeliveryApp.Data.Model;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DeliveryApp.Services
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
            //http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
            IsLoggedIn = false;
        }

        public async Task<bool> Login(string username, string password)
        {
            var response = await http.PostAsJsonAsync(loginUrl, new { UserName = username, Password = password });
            if (response.IsSuccessStatusCode)
            {
                Log.Debug("Login succefully");
                User = await response.Content.ReadFromJsonAsync<UserModel>();
                IsLoggedIn = true;

                if (User.Logo != null)
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
            IsLoggedIn = false;
        }

        public UserModel getUserByEmail(string userEmail)
        {
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
            return http.GetFromJsonAsync<UserModel>($"api/User/OneByEmail/{userEmail}").Result;
        }

        public async Task<bool> Update(UserModel user)
        {
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(user.Name), "Name");
            multipartContent.Add(new StringContent(user.Email), "Email");
            multipartContent.Add(new StringContent(user.PhoneNumber), "PhoneNumber");
            multipartContent.Add(new StringContent(user.Street), "Street");
            multipartContent.Add(new StringContent(user.City), "City");
            multipartContent.Add(new StringContent(user.State), "State");
            multipartContent.Add(new StringContent(user.Country), "Country");
            multipartContent.Add(new StringContent(user.PostCode), "PostCode");

            var img = new StreamContent(user.UploadImg?.OpenReadStream());
            img.Headers.ContentType = new MediaTypeHeaderValue(user.UploadImg.ContentType);
            multipartContent.Add(content: img, "UploadImg", fileName: user.UploadImg.Name);
            //http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
            HttpResponseMessage response = await http.PutAsync("api/User/UpdatedOne", multipartContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            Log.Debug("Update failed,sign up again");
            return false;
        }
    }
}
