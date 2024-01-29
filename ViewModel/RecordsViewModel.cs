using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Object.MyData;
using NET_MAUI_BLE.Pages;


namespace NET_MAUI_BLE.ViewModel;

public partial class RecordsViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<string> items;

    private DatabaseManager databaseManager;

    public RecordsViewModel(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
        items = new ObservableCollection<string>();
        // TODO - Implement: Load data when the app got started (initial stage)
        // TODO - Implement: Add loading dynamic image
        foreach(var record in databaseManager.GetRecords()) 
        {
            Items.Add(record.recordName);
        }
    }

    [RelayCommand]
    void Delete(string path)
    {
        //Items.Remove(path);
        //databaseManager.DeleteData(path);
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