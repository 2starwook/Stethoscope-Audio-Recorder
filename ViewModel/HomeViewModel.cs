using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;
using System.Text.Json;

using NET_MAUI_BLE.Object.Wifi;
using NET_MAUI_BLE.Object.DB;
using NET_MAUI_BLE.Message.DbMessage;
using NET_MAUI_BLE.API;


namespace NET_MAUI_BLE.ViewModel;

public partial class HomeViewModel : ObservableObject
{
	public HomeViewModel()
	{
        this.databaseManager = DependencyService.Get<DBManager>();
        wifiController = new WifiController();
        AudioControlVisible = false;
        audioSource = "";
    }

    private DBManager databaseManager;
    private WifiController wifiController;
    [ObservableProperty]
    bool audioControlVisible;
    [ObservableProperty]
    string audioSource;

    [RelayCommand]
    async Task Appearing()
    {
        try
        {
            await databaseManager.LoadDataAsync();
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
        var recordName = $"recording_{TimeAPI.GetCurrentDateTime()}";
        var recordId = await databaseManager.AddRecordAsync(AudioSource, recordName);
        WeakReferenceMessenger.Default.Send(new AddRecordMessage(recordId));
    }
}