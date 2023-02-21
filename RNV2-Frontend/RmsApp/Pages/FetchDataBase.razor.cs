using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
// using System.Net.Http;
using System.Net.Http.Json;


namespace RmsApp.Pages
{
    public class FetchDataBase : ComponentBase
    {
        [Inject]
        protected HttpClient Http { get; set; }
        public List<TabPath> Paths = new List<TabPath>
        {
        new TabPath { Title = "Home", Path = "/" },
        new TabPath { Title = "Weather Forcast", Path = "/fetchdata" },
        };

        public WeatherForecast[]? forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
        }

        public class WeatherForecast
        {
            public DateOnly Date { get; set; }

            public int TemperatureC { get; set; }

            public string? Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }

    }
}