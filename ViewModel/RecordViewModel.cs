using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NET_MAUI_BLE.API;
using NET_MAUI_BLE.Object.DB;


namespace NET_MAUI_BLE.ViewModel;

[QueryProperty("RecordId", "RecordId")]
public partial class RecordViewModel : ObservableObject
{
    public RecordViewModel()
    {
        _databaseManager = DependencyService.Get<DBManager>();
    }

    private DBManager _databaseManager;
    [ObservableProperty]
	private string recordId;
    [ObservableProperty]
    private string audioSource;

    [RelayCommand]
    void Appearing()
    {
        try
        {
            UIAPI.HideTab();
            var fileName = $"{RecordId}.wav";
            var binaryData = _databaseManager.currentRecords[RecordId].BinaryData;
            FileAPI.WriteCacheData(fileName, binaryData);
            AudioSource = FileAPI.GetCachePath(fileName);
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
}