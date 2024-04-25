using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Realms;
using System.Diagnostics;

using NET_MAUI_BLE.Pages;
using NET_MAUI_BLE.Models;
using NET_MAUI_BLE.Services;
using NET_MAUI_BLE.API;


namespace NET_MAUI_BLE.ViewModel;

public partial class RecordsViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isLoading = false;

    [ObservableProperty]
    private string connectionStatusIcon = "wifi_on.png";

    [ObservableProperty]
    private IQueryable<Item> items;

    [ObservableProperty]
    public string dataExplorerLink = RealmService.DataExplorerLink;

    private Realm realm;
    private bool isOnline = true;

    [RelayCommand]
    void Appearing()
    {
        realm = RealmService.GetMainThreadRealm();
        Items = realm.All<Item>().OrderBy(i => i.Id);
    }

    [RelayCommand]
    async Task Delete(Item item)
    {
        Trace.WriteLine("Delete command executed");
        await RealmAPI.Delete(realm, item);
    }

    [RelayCommand]
    async Task Tap(Item item)
    {
        Trace.WriteLine("Tap command executed");
        await Shell.Current.GoToAsync($"{nameof(RecordPage)}?",
            new Dictionary<string, object>
            {
                ["item"] = item,
            });
    }

    [RelayCommand]
    async Task AddFile()
    {
        await Shell.Current.GoToAsync($"/{nameof(AddRecordPage)}");
    }

    [RelayCommand]
    public void ChangeConnectionStatus()
    {
        isOnline = !isOnline;

        if (isOnline)
        {
            realm.SyncSession.Start();
        }
        else
        {
            realm.SyncSession.Stop();
        }

        ConnectionStatusIcon = isOnline ? "wifi_on.png" : "wifi_off.png";
    }
}