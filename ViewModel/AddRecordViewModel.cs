using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using NET_MAUI_BLE.Object.DB;
using NET_MAUI_BLE.Message.DbMessage;
using NET_MAUI_BLE.API;


namespace NET_MAUI_BLE.ViewModel;

public partial class AddRecordViewModel : ObservableObject
{
    [ObservableProperty]
	private string recordName;

    [ObservableProperty]
	private string filePath;

    [ObservableProperty]
    private string fileButtonText = "Select a File";

    private DBManager _databaseManager = DependencyService.Get<DBManager>();

    public void Refresh()
    {
        FileButtonText = "Select a File";
        RecordName = "";
        FilePath = "";
    }

    [RelayCommand]
    void Appearing()
    {
        try
        {
            UIAPI.HideTab();
            Refresh();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e}");
        }
    }

    [RelayCommand]
    void Disappearing()
    {
        try
        {
            UIAPI.ShowTab();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e}");
        }
    }

    [RelayCommand]
	async Task Submit()
	{
        var recordId = await _databaseManager.AddRecordAsync(FilePath, RecordName);
        WeakReferenceMessenger.Default.Send(new AddRecordMessage(recordId));
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task SelectFile()
    {
        var path = await DBManager.ImportAudioFile();
        if (string.IsNullOrWhiteSpace(path))
            return;
        FilePath = FileButtonText = path;
    }
}