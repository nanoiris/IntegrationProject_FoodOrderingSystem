using RestaurantDaoBase.Models;
using Serilog;
using System.Net.Http.Json;

namespace OssApp.Services
{
    public class RestService<T>
    {
        internal HttpClient http;
        public string server;
        
        public RestService(string server)
        {
            this.server = server;
            http = new HttpClient();
            http.BaseAddress = new Uri(server);

            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
        }

        public async Task<List<T>> List(string listUrl)
        {
            var response = await http.GetAsync(listUrl);
            List<T> rows = null;
            if (response.IsSuccessStatusCode)
            {
                Log.Debug("RestService.List");
                try
                {
                    rows = response.Content.ReadFromJsonAsync<List<T>>()
                        .GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Log.Debug($"RestService.List : {ex.Message}");
                }
            }
            if (rows == null)
            {
                rows = new List<T>();
            }
            return rows;
        }

        public bool DeleteOne(string url)
        {
            Log.Debug($"RestService.DeleteOne {url}");
            var response = http.DeleteAsync(url).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool UpdateOne(string url, MultipartFormDataContent content)
        {
            var response = http.PutAsync(url, content).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public string AddNewOne(string url, MultipartFormDataContent content)
        {
            Log.Debug($"Enter RestService : AddNewOne : {url}");
            var response = http.PostAsync(url, content).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                Log.Debug("Add new one succefully");
                var result = response.Content.ReadFromJsonAsync<AppResult>().GetAwaiter().GetResult();
                if (result.IsSuccess)
                {
                    return result.Message;
                }
            }
            return null;
        }
    }
}
