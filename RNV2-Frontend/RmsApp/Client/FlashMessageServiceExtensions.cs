using Microsoft.Extensions.DependencyInjection;
using RmsApp.Services;

public static class FlashMessageServiceExtensions
{
    public static IServiceCollection AddFlashMessageService(this IServiceCollection services)
    {
        return services.AddScoped<IFlashMessageService, FlashMessageService>();
    }
}
