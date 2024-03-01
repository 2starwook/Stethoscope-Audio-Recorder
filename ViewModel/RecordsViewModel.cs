using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Object.MyData;
using NET_MAUI_BLE.Pages;
using Object.MyRecords;

namespace NET_MAUI_BLE.ViewModel;
public partial class RecordsViewModel : ObservableObject
{
    public RecordsViewModel(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    private DatabaseManager databaseManager;
    [ObservableProperty]
    private ObservableCollection<RecordInfo> records;

    [RelayCommand]
    async Task Appearing()
    {
        // TODO: Implement(Show Loading Image)
        await databaseManager.LoadDataAsync();
        Records = new ObservableCollection<RecordInfo>();
        try
        {
            foreach (var record in databaseManager.currentRecords.Values)
            {
                Records.Add(new RecordInfo(record.RecordName, record.GetId()));
            }
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }

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