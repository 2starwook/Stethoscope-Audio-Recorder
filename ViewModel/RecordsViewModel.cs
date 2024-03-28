using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using NET_MAUI_BLE.Message.DbMessage;
using NET_MAUI_BLE.Object.DB;
using NET_MAUI_BLE.Pages;
using NET_MAUI_BLE.Object.View;


namespace NET_MAUI_BLE.ViewModel;

public partial class RecordsViewModel : ObservableObject, IRecipient<AddRecordMessage>
{
    public RecordsViewModel()
    {
        this.databaseManager = DependencyService.Get<DBManager>();
        WeakReferenceMessenger.Default.Register(this);
        IsLoading = false;
    }

    private DBManager databaseManager;
    [ObservableProperty]
    private ObservableCollection<RecordInfo> records;
    [ObservableProperty]
    private bool isLoading;

    [RelayCommand]
    async Task Appearing()
    {
        if (Records == null)
        {
            await LoadDataAsync();
        }
    }

    private void AddRecord(Record record)
    {
        Records.Add(new RecordInfo(record.RecordName, record.GetId()));
    }

    private async Task LoadDataAsync()
    {
        IsLoading = true;
        await databaseManager.LoadRecordDataAsync();
        Records = new ObservableCollection<RecordInfo>();
        try
        {
            foreach (var record in databaseManager.currentRecords.Values)
            {
                AddRecord(record);
            }
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e}");
        }
        IsLoading = false;
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
        await Shell.Current.GoToAsync($"/{nameof(AddRecordPage)}");
    }

    public void Receive(AddRecordMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Record addedRecord;
            var res = databaseManager.currentRecords
                        .TryGetValue(message.Value, out addedRecord);
            AddRecord(addedRecord);
        });
    }
}