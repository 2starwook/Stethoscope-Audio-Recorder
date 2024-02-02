using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Object.MyData;
using NET_MAUI_BLE.Pages;


namespace NET_MAUI_BLE.ViewModel;

public class Item
{
    public string name {get; set;}
    public string id {get; set;}
    public Item(string name, string id)
    {
        this.name = name;
        this.id = id;
    }
}

public partial class RecordsViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<Item> items;

    private DatabaseManager databaseManager;

    public RecordsViewModel(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
        items = new ObservableCollection<Item>();
        // TODO - Implement: Load data when the app got started (initial stage)
        // TODO - Implement: Add loading dynamic image
        foreach(var record in databaseManager.currentRecords) 
        {
            Items.Add(new Item(record.recordName, record.Id.ToString()));
        }
    }

    [RelayCommand]
    async Task Delete(string id)
    {
        await databaseManager.DeleteRecordDataAsync(id);
        Items.Remove(Items.SingleOrDefault(i => i.id == id));
    }

    [RelayCommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(RecordPage)}?Text={s}");
    }

    [RelayCommand]
    async Task AddFile()
    {
        await Shell.Current.GoToAsync($"{nameof(AddPage)}");
    }
}