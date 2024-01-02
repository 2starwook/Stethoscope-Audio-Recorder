using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using Shiny;
using Shiny.Infrastructure;
using Plugin.Maui.Audio;

namespace NET_MAUI_BLE;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.UseMicrocharts()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton(AudioManager.Current);
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
		services.AddSingleton<NET_MAUI_BLE.Pages.HomePage>();
		services.AddSingleton<NET_MAUI_BLE.Pages.AudioPage>();
	}
}
