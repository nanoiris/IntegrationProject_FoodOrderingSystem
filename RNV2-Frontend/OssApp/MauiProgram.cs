using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog;
using Radzen;
using OssApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace OssApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        SetupSerilog();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddSingleton<AuthService>(service =>
           new AuthService("http://localhost:5191")
        );
        builder.Services.AddScoped<RoleService>(service =>
           new RoleService("http://localhost:5191")
        );
        
        builder.Services.AddSingleton<RestCategoryService>(service =>
           new RestCategoryService("http://localhost:5064")
        );

        builder.Services.AddSingleton<RestaurantService>(service =>
          new RestaurantService("http://localhost:5064")
       );

        builder.Services.AddScoped<DialogService>();
        builder.Services.AddScoped<NotificationService>();
        builder.Services.AddScoped<TooltipService>();
        builder.Services.AddScoped<ContextMenuService>();

        /*
        builder.Services.AddScoped<AuthService>(service =>
		   new AuthService("http://fsd05rnv.eastus.cloudapp.azure.com:5191")
		);
		builder.Services.AddScoped<RoleService>(service =>
		   new RoleService("http://fsd05rnv.eastus.cloudapp.azure.com:5191")
		);
		builder.Services.AddScoped<RestCategoryService>(service =>
		   new RestCategoryService("http://fsd05rnv.eastus.cloudapp.azure.com:5064")
		);
        */
        builder.Services.AddMauiBlazorWebView();


#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        builder.Logging.AddSerilog(dispose: true);

        return builder.Build();
    }
    private static void SetupSerilog()
    {
        var flushInterval = new TimeSpan(0, 0, 1);
        var file = Path.Combine(FileSystem.AppDataDirectory, "d:\\tmp\\oss.log");

        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.File(file, flushToDiskInterval: flushInterval, encoding: System.Text.Encoding.UTF8, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 22)
        .CreateLogger();
    }
}
