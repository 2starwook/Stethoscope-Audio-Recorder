using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Realms;
using System.Diagnostics;

using NET_MAUI_BLE.Object.Wifi;
using NET_MAUI_BLE.Object.DB;
using NET_MAUI_BLE.Message.DbMessage;
using NET_MAUI_BLE.API;
using NET_MAUI_BLE.Models;
using NET_MAUI_BLE.Services;

namespace NET_MAUI_BLE.ViewModel;

public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
    private string audioSource = "";

    //private DBManager databaseManager = DependencyService.Get<DBManager>();
    private WifiController wifiController = new WifiController();
    public delegate void DataReceivedEvent(bool isSuccessful);
    public event DataReceivedEvent AudioReceivedEvent;
    private Realm realm;
    [ObservableProperty]
    private string connectionStatusIcon = "wifi_on.png";

    [ObservableProperty]
    private bool isShowAllTasks;

    [ObservableProperty]
    private IQueryable<Item_> items;

    [ObservableProperty]
    public string dataExplorerLink = RealmService.DataExplorerLink;

    [ObservableProperty]
    private Item_ initialItem;

    [ObservableProperty]
    private string summary;

    [ObservableProperty]
    private string pageHeader;

    private string currentUserId;
    private bool isOnline = true;

    [RelayCommand]
    async Task Appearing()
    {
        try
        {
            await RealmService.Init();

            realm = RealmService.GetMainThreadRealm();
            currentUserId = RealmService.CurrentUser.Id;
            Items = realm.All<Item_>().OrderBy(i => i.Id);

            var currentSubscriptionType = RealmService.GetCurrentSubscriptionType(realm);
            IsShowAllTasks = currentSubscriptionType == SubscriptionType.All;
            //await databaseManager.LoadDataAsync();
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
            await AddItem(recordName, FileAPI.ReadData(AudioSource));
            //var recordId = await databaseManager.AddRecordAsync(AudioSource, recordName);
            //WeakReferenceMessenger.Default.Send(new AddRecordMessage(recordId));
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            isSuccessful = false;
        }
        AudioReceivedEvent.Invoke(isSuccessful);
    }

    private async Task AddItem(string recordName, byte[] binaryData)
    {
        var realm = RealmService.GetMainThreadRealm();
        await realm.WriteAsync(() =>
        {
            realm.Add(new Item_()
            {
                OwnerId = RealmService.CurrentUser.Id,
                RecordName = recordName,
                BinaryData = binaryData,
                Summary = "TEST",
            });
        });
    }
}
