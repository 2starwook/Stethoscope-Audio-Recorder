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
        // var baseAddress = "http://127.0.0.1:8000";
        var baseAddress = "http://192.168.4.1:1337";
        //wifiController = new WifiController();
        httpClientManager = new HttpClientManager(baseAddress);
        AudioControlVisible = false;
        audioSource = "";
    }

    // WifiController wifiController;
    HttpClientManager httpClientManager;

    [ObservableProperty]
    string testLabel;
    [ObservableProperty]
    bool audioControlVisible;
    [ObservableProperty]
    string audioSource;

    partial void OnAudioSourceChanged(string value)
    {
        if (value != "")
        {
            AudioControlVisible = true;
        }
        else
        {
            AudioControlVisible = false;
        }
    }

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
        AudioSource = await httpClientManager.GetAudio();
    }
}