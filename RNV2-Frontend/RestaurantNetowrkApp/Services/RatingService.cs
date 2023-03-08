using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog;

namespace RestaurantNetowrkApp.Services
{
    public class RatingService
    {
        internal HttpClient http;
        public string server;
        public RatingService(string server)
        {
            this.server = server;
            http = new HttpClient();
            http.BaseAddress = new Uri(server);
            //http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
        }

        public async Task<bool> rateRestaurant(string restId, string username, int value)
        {
            var requestBody = new
            {
                restaurantId = restId,
                value = value,
                createBy = username
            };
            var response = await http.PostAsJsonAsync("api/rate/torestaurant", requestBody);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            Log.Debug($"Rate restaurant failed. restID:{restId}, user:{username}");
            return false;
        }

        public async Task<bool> rateMenuItem(string menuItemId, string username, int value,string restId)
        {
            var requestBody = new
            {
                restaurantId = restId,
                menuItemId = menuItemId,
                value = value,
                createBy = username
            };
            var response = await http.PostAsJsonAsync("api/rate/toMenuItem", requestBody);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            Log.Debug($"Rate restaurant failed. menuItemId:{menuItemId}, user:{username}");
            return false;
        }

        public async Task<string[]> getRatingPercentages(string restId)
        {
            var response = await http.GetAsync($"api/rate/Percentages/{restId}");
            if(response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<string[]>(responseBody);
            }
            return null;
        }

        public async Task<string> getRatingTotal(string restId)
        {
            var response = await http.GetAsync($"api/rate/Total/{restId}");
            if (response.IsSuccessStatusCode)
            {
                return  await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            return "0";
        }


    }
}
