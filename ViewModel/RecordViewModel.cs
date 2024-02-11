using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

using Object.MyAudio;
using MyAPI;
using Object.MyData;


namespace NET_MAUI_BLE.ViewModel;

[QueryProperty("RecordId", "RecordId")]
public partial class RecordViewModel : ObservableObject
{
    public RecordViewModel(IAudioManager audioManager, DatabaseManager databaseManager)
    {
        audioController = new AudioController(audioManager);
        _databaseManager = databaseManager;
		playText = "Play";
        // TODO - Install Cache data using Async once the page loaded
    }

    private AudioController audioController;
    private DatabaseManager _databaseManager;

    [ObservableProperty]
	string recordId;

	[ObservableProperty]
	string playText;

    [RelayCommand]
    void Appearing()
    {
        try
        {
            MYAPI.UIAPI.HideTab();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }

    [RelayCommand]
    void Disappearing()
    {
        try
        {
            MYAPI.UIAPI.ShowTab();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }

    [RelayCommand]
	async Task Play(string recordId)
    {
        var fileName = $"{recordId}.wav";
        var binaryData = _databaseManager.currentRecords[recordId].binaryData;
        FileAPI.WriteCacheData(fileName, binaryData);
        await audioController.OpenFile(FileAPI.GetCachePath(fileName));
		audioController.Play();
		//audioController.AddEventHandler(new EventHandler(HandlePlayEnded));
		//PlayText = "Stop";
		// TODO - Add Stop / Pause Button
	}

	//void HandlePlayEnded(object sender, EventArgs e){
	//	PlayText = "Play";
	//}

    [RelayCommand]
	async Task Share(string recordId)
    {
        var tempName = "file.wav";
        var binaryData = _databaseManager.currentRecords[recordId].binaryData;
        await StorageAPI.ExportData(tempName, binaryData);
    }
}