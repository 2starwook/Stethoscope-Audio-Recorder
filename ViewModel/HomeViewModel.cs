using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;
using System.Text.Json;

using NET_MAUI_BLE.Object.Wifi;


namespace NET_MAUI_BLE.ViewModel;

public partial class HomeViewModel : ObservableObject
{
	public HomeViewModel()
	{
        TestLabel = "Empty";
        wifiController = new WifiController();
        AudioControlVisible = false;
        audioSource = "";
    }

    private WifiController wifiController;
    [ObservableProperty]
    string testLabel;
    [ObservableProperty]
    bool audioControlVisible;
    [ObservableProperty]
    string audioSource;

    [RelayCommand]
    void Appearing()
    {
        try
        {

        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }

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

        System.Diagnostics.Debug.WriteLine("Test Button clicked");
        //await httpClientManager.GetAsync("/");
    }

    [RelayCommand]
    async Task GetAudio()
    {
        System.Diagnostics.Debug.WriteLine("GetAudio Button clicked");
        AudioSource = await wifiController.GetAudio();
    }
}