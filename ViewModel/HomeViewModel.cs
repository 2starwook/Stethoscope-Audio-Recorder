using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Realms;
using System.Diagnostics;

using NET_MAUI_BLE.Object.Wifi;
using NET_MAUI_BLE.API;
using NET_MAUI_BLE.Services;

namespace NET_MAUI_BLE.ViewModel;

public partial class HomeViewModel : ObservableObject
{
    private WifiController wifiController = new WifiController();

    public delegate void DataReceivedEvent(bool isSuccessful);

    public event DataReceivedEvent AudioReceivedEvent;

    private Realm realm;

    [ObservableProperty]
    private string connectionStatusIcon = "wifi_on.png";

    [RelayCommand]
    async Task Appearing()
    {
        try
        {
            await RealmService.Init();
            realm = RealmService.GetMainThreadRealm();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }

    [RelayCommand]
    async Task GetAudio()
    {
        Trace.WriteLine("GetAudio Button clicked");
        bool isSuccessful = true;
        try
        {
            var recordName = $"recording_{TimeAPI.GetCurrentDateTime()}";
            var audioSource = await wifiController.GetAudio();
            await RealmAPI.Add(realm, recordName, FileAPI.ReadData(audioSource));
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            isSuccessful = false;
        }
        AudioReceivedEvent.Invoke(isSuccessful);
    }
}
