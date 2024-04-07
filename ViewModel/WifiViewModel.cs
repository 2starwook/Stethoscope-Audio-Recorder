using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NET_MAUI_BLE.Object.Wifi;


namespace NET_MAUI_BLE.ViewModel;

public partial class WifiViewModel : ObservableObject
{
	public WifiViewModel()
	{
        TestLabel = "Empty";
        //var baseAddress = "http://127.0.0.1:8000";
        var baseAddress = "http://192.168.4.1:1337";
        //wifiController = new WifiController();
        httpClientManager = new HttpClientManager(baseAddress);
    }

    // WifiController wifiController;
    HttpClientManager httpClientManager;

    [ObservableProperty]
    string testLabel;

    [RelayCommand]
    async Task Test()
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                completed = true
            }),
            Encoding.UTF8,
            "application/json");

        System.Diagnostics.Debug.WriteLine("Test1 Button clicked");
        //await httpClientManager.GetAsync("/");
    }

    [RelayCommand]
    async Task GetAudio()
    {
        System.Diagnostics.Debug.WriteLine("GetAudio Button clicked");
        //await httpClientManager.GetAsync("/");
    }

    [RelayCommand]
    async Task GetStreamAudio()
    {
        System.Diagnostics.Debug.WriteLine("GetStreamAudio Button clicked");
        //await httpClientManager.GetAsync("/");
    }

    [RelayCommand]
    async Task SendL()
    {
        System.Diagnostics.Debug.WriteLine("SendL Button clicked");
        await httpClientManager.GetAsync("/L");
    }

    [RelayCommand]
    async Task SendH()
    {
        System.Diagnostics.Debug.WriteLine("SendH Button clicked");
        await httpClientManager.GetAsync("/H");
    }

}