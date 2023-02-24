using Microsoft.Extensions.Logging;
using RestaurantNetowrkApp.Data;
using RestaurantNetowrkApp.Services;
using RestaurantNetowrkApp.Services.IServices;
using Serilog;
using Serilog.Events;

namespace RestaurantNetowrkApp;

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

        builder.Services.AddMauiBlazorWebView();

		//builder.Services.AddHttpClient<IRestaurantService, RestaurantService>(client =>
		//{
		//	client.BaseAddress = new Uri("https://localhost:5064/");
		//});

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        builder.Logging.AddSerilog(dispose: true);
        Log.Debug("in progr");
        return builder.Build();
	}

    private static void SetupSerilog()
    {
        var flushInterval = new TimeSpan(0, 0, 1);
        //var file = Path.Combine(FileSystem.AppDataDirectory, "MyApp.log");
        var file = Path.Combine(@"C:\Users\pxtod\Documents\tmp\my.log");

        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.File(file, flushToDiskInterval: flushInterval, encoding: System.Text.Encoding.UTF8, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 22)
        .CreateLogger();
    }
}
