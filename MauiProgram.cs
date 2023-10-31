using Microsoft.Extensions.Logging;
using Shiny;
using Shiny.Infrastructure;

namespace NET_MAUI_BLE;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddShiny();
		builder.Services.AddDependencies();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	private static void AddShiny(this IServiceCollection services)
	{
		services.AddShinyCoreServices();
		services.AddBluetoothLE();
	}

	private static void AddDependencies(this IServiceCollection services)
	{
		services.AddSingleton<NET_MAUI_BLE.MainPage>();
	}
}
