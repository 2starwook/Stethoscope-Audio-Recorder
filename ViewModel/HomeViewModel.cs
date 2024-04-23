using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;
using System.Text.Json;

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
        Trace.WriteLine("GetAudio Button clicked");
