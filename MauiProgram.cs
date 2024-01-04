﻿using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using Shiny;
using Shiny.Infrastructure;
using Plugin.Maui.Audio;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Maui.Media;

namespace NET_MAUI_BLE;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.UseMicrocharts()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddShiny();
		builder.Services.AddSingleton(AudioManager.Current);
		builder.Services.AddSingleton(FileSaver.Default);
		builder.Services.AddSingleton(FolderPicker.Default);
		builder.Services.AddSingleton(FilePicker.Default);
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
		services.AddSingleton<NET_MAUI_BLE.Pages.TestPage>();
	}
}
