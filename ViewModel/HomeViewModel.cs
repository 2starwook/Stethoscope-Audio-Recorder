using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using NET_MAUI_BLE.Object.Wifi;
using NET_MAUI_BLE.Object.DB;
using NET_MAUI_BLE.Message.DbMessage;
using NET_MAUI_BLE.API;
using System.Diagnostics;

namespace NET_MAUI_BLE.ViewModel;

public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
    private string audioSource = "";

    private DBManager databaseManager = DependencyService.Get<DBManager>();
    private WifiController wifiController = new WifiController();
    public delegate void DataReceivedEvent(bool isSuccessful);
    public event DataReceivedEvent AudioReceivedEvent;

    [RelayCommand]
    async Task Appearing()
    {
        try
        {
            await databaseManager.LoadDataAsync();
        }
        catch (Exception e)
        {
            Debug.WriteLine($"{e}");
        }
    }

    [RelayCommand]
    async Task GetAudio()
    {
        Trace.WriteLine("GetAudio Button clicked");
        bool isSuccessful = true;
        try
        {
            AudioSource = await wifiController.GetAudio();
            var recordName = $"recording_{TimeAPI.GetCurrentDateTime()}";
            var recordId = await databaseManager.AddRecordAsync(AudioSource, recordName);
            WeakReferenceMessenger.Default.Send(new AddRecordMessage(recordId));
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            isSuccessful = false;
        }
        AudioReceivedEvent.Invoke(isSuccessful);
    }

}
