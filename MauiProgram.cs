using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Plugin.BLE;


namespace NET_MAUI_BLE;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton(CrossBluetoothLE.Current);
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

	private static void AddDependencies(this IServiceCollection services)
	{
		services.AddSingleton<NET_MAUI_BLE.Pages.HomePage>();
		services.AddSingleton<NET_MAUI_BLE.Pages.AudioPage>();
		services.AddSingleton<NET_MAUI_BLE.Pages.RecordsPage>();
		services.AddSingleton<NET_MAUI_BLE.Pages.AddRecordPage>();
		services.AddSingleton<NET_MAUI_BLE.Pages.AddPatientPage>();
		services.AddSingleton<NET_MAUI_BLE.Pages.RecordPage>();
		services.AddSingleton<NET_MAUI_BLE.Pages.TestPage>();
        services.AddSingleton<NET_MAUI_BLE.Pages.WifiPage>();

        services.AddSingleton<NET_MAUI_BLE.ViewModel.RecordViewModel>();
		services.AddSingleton<NET_MAUI_BLE.ViewModel.RecordsViewModel>();
		services.AddSingleton<NET_MAUI_BLE.ViewModel.HomeViewModel>();
		services.AddSingleton<NET_MAUI_BLE.ViewModel.TestViewModel>();
		services.AddSingleton<NET_MAUI_BLE.ViewModel.AddRecordViewModel>();
		services.AddSingleton<NET_MAUI_BLE.ViewModel.AddPatientViewModel>();
        services.AddSingleton<NET_MAUI_BLE.ViewModel.WifiViewModel>();

        services.AddSingleton<Object.MyData.DatabaseManager>();
	}
}
