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
        _audioController = new AudioController(audioManager);
        _databaseManager = databaseManager;
		playText = "Play";
    }

    private AudioController _audioController;
    private DatabaseManager _databaseManager;
    [ObservableProperty]
	private string recordId;
	[ObservableProperty]
	private string playText;

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
        var binaryData = _databaseManager.currentRecords[recordId].BinaryData;
        FileAPI.WriteCacheData(fileName, binaryData);
        await _audioController.OpenFile(FileAPI.GetCachePath(fileName));
		_audioController.Play();
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
        var binaryData = _databaseManager.currentRecords[recordId].BinaryData;
        await StorageAPI.ExportData(tempName, binaryData);
    }
}