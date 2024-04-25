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
    private IQueryable<Item> items;

    private Realm realm;

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
        FilesystemAPI.DeleteFile(item.GetId());
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
}