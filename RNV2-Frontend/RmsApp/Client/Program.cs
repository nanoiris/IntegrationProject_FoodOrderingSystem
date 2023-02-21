using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RmsApp.Services;
using RmsApp;
using Microsoft.Extensions.Logging;

namespace RmsApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            var host = builder.Build();
            builder.Logging.ClearProviders();
            var logger = host.Services.GetRequiredService<ILoggerFactory>()
                .CreateLogger<Program>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // Register the mock ICategoryService
            builder.Services.AddScoped<ICategoryService, CategoryService>(x=>
            new CategoryService(logger));
           
            logger.LogInformation("Enter Log..."); 

            await host.RunAsync();
        }
    }
}