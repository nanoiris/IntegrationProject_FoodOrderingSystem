using Microsoft.Extensions.Logging;
using DeliveryApp.Data;
using DeliveryApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});
        builder.Services.AddSingleton<AuthService>(service =>
           new AuthService("http://ec2-18-214-61-45.compute-1.amazonaws.com:5191/")
        );
        builder.Services.AddSingleton<Utils>(service =>
           new Utils()
        );

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		//builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
