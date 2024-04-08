using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

using NET_MAUI_BLE.Object.Audio;
using NET_MAUI_BLE.API;
using NET_MAUI_BLE.Object.DB;


namespace NET_MAUI_BLE.ViewModel;

[QueryProperty("RecordId", "RecordId")]
public partial class RecordViewModel : ObservableObject
{
    public RecordViewModel(IAudioManager audioManager)
    {
        _audioController = new AudioController(audioManager);
        _databaseManager = DependencyService.Get<DBManager>();
        PlayingView = false;
        PauseResumeText = "Pause";
    }

    private AudioController _audioController;
    private DBManager _databaseManager;
    [ObservableProperty]
	private string recordId;
    [ObservableProperty]
    private bool playingView;
    [ObservableProperty]
    private string pauseResumeText;

    [RelayCommand]
    void Appearing()
    {
        try
        {
            UIAPI.HideTab();
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
    async Task Share(string recordId)
    {
        var tempName = "file.wav";
        var binaryData = _databaseManager.currentRecords[recordId].BinaryData;
        await StorageAPI.ExportData(tempName, binaryData);
    }

    [RelayCommand]
	async Task Play(string recordId)
    {
        PlayingView = true;
        var fileName = $"{recordId}.wav";
        var binaryData = _databaseManager.currentRecords[recordId].BinaryData;
        FileAPI.WriteCacheData(fileName, binaryData);
        await _audioController.OpenFile(FileAPI.GetCachePath(fileName));
        _audioController.Play();
        _audioController.AddEventHandler(new EventHandler(HandlePlayEnded));
    }

    void HandlePlayEnded(object sender, EventArgs e)
    {
        PlayingView = false;
    }

    #pragma warning disable CS1998
    [RelayCommand]
    async Task PauseResume()
    {
        if (_audioController.IsPlaying())
        {
            _audioController.Pause();
            PauseResumeText = "Resume";
        }
        else
        {
            _audioController.Play();
            PauseResumeText = "Pause";
        }
    }
    #pragma warning restore CS1998

    #pragma warning disable CS1998
    [RelayCommand]
    async Task Stop()
    {
        PlayingView = false;
        _audioController.Stop();
    }
    #pragma warning restore CS1998
}