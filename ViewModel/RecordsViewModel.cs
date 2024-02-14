using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Object.MyData;
using NET_MAUI_BLE.Pages;


namespace NET_MAUI_BLE.ViewModel;

public class RecordInfo
{
    public string Name {get; set;}
    public string Id {get; set;}
    public RecordInfo(string name, string id)
    {
        this.Name = name;
        this.Id = id;
    }
}

public partial class RecordsViewModel : ObservableObject
{
    public RecordsViewModel(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
        this.records = new ObservableCollection<RecordInfo>();
        // TODO - Implement: Load data when the app got started (initial stage)
        // TODO - Implement: Add loading dynamic image
        foreach(var record in databaseManager.currentRecords.Values) 
        {
            this.records.Add(new RecordInfo(record.RecordName, record.GetId()));
        }
    }

    private DatabaseManager databaseManager;
    [ObservableProperty]
    private ObservableCollection<RecordInfo> records;

    [RelayCommand]
    async Task Delete(string id)
    {
        await databaseManager.DeleteRecordAsync(id);
        Records.Remove(Records.SingleOrDefault(i => i.Id == id));
    }

    [RelayCommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(RecordPage)}?",
            new Dictionary<string, object>
            {
                ["RecordId"] = s,
            });
    }

    [RelayCommand]
    async Task AddFile()
    {
        await Shell.Current.GoToAsync($"{nameof(AddRecordPage)}");
    }
}