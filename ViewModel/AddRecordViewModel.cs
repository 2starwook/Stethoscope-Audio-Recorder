using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Realms;

using NET_MAUI_BLE.API;
using NET_MAUI_BLE.Services;


namespace NET_MAUI_BLE.ViewModel;
public partial class AddRecordViewModel : ObservableObject
{
    [ObservableProperty]
	private string recordName;

    [ObservableProperty]
	private string filePath;

    [ObservableProperty]
    private string fileButtonText = "Select a File";

    private Realm realm;

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
            realm = RealmService.GetMainThreadRealm();
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
        await RealmAPI.Add(realm, RecordName, FileAPI.ReadData(FilePath));
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task SelectFile()
    {
        var srcPath = await StorageAPI.GetFilePath();
        var dstPath = FileAPI.GetAudioPath($"{FileAPI.GetUniqueID()}.wav");
        File.Copy(srcPath.ToString(), dstPath);
        if (string.IsNullOrWhiteSpace(dstPath))
            return;
        FilePath = FileButtonText = dstPath;
    }
}