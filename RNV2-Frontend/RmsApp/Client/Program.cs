using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RmsApp.Services;
using RmsApp;
using Microsoft.Extensions.Logging;
using Radzen;


namespace RmsApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // Register the mock ICategoryService

            builder.Services.AddHttpClient<ICategoryService, CategoryService>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });
            builder.Services.AddHttpClient<IItemService, ItemService>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });
            builder.Services.AddHttpClient<IRestaurantService, RestaurantService>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });
            builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddHttpClient<AuthService>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });
            builder.Services.AddFlashMessageService();
            builder.Services.AddScoped<NotificationService>();


            var host = builder.Build();
            // builder.Logging.ClearProviders();
            var logger = host.Services.GetRequiredService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogInformation("enter Log from program.cs...");

            await host.RunAsync();
        }
    }
}