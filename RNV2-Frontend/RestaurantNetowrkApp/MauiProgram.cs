﻿using Microsoft.Extensions.Logging;
using RestaurantNetowrkApp.Data;
using RestaurantNetowrkApp.Services;
using RestaurantNetowrkApp.Services.IServices;

namespace RestaurantNetowrkApp;

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

        builder.Services.AddMauiBlazorWebView();

		//builder.Services.AddHttpClient<IRestaurantService, RestaurantService>(client =>
		//{
		//	client.BaseAddress = new Uri("https://localhost:5064/");
		//});

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        return builder.Build();
	}
}
