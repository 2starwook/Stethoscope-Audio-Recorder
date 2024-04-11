using CommunityToolkit.Mvvm.Input;

using NET_MAUI_BLE.Object.Audio;
using NET_MAUI_BLE.API;
using Plugin.Maui.Audio;


namespace NET_MAUI_BLE.Controls;

public partial class AudioControl : ContentView
{
    public AudioControl()
    {
        _audioController = new AudioController(AudioManager.Current);
        PlayingView = false;
        PauseResumeText = "Pause";
        InitializeComponent();
    }
    // TODO - Implement: Showing runtime of audio and where it is at

    private AudioController _audioController;
    private bool _playingView;
    public bool PlayingView {
        get => _playingView;
        set
        {
            _playingView = value;
            OnPropertyChanged(nameof(PlayingView));
        }
    }
    private string _pauseResumeText;
    public string PauseResumeText {
        get => _pauseResumeText;
        set
        {
            _pauseResumeText = value;
            OnPropertyChanged(nameof(PauseResumeText));
        }
    }

    public string Source
    {
        get => GetValue(SourceProperty) as string;
        set => SetValue(SourceProperty, value);
    }

    public static readonly BindableProperty SourceProperty = BindableProperty.Create(
        nameof(Source), typeof(string), typeof(AudioControl),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (AudioControl)bindable;
        }
        //validateValue: (bindable, value) =>
        //{
        //    return FileAPI.isExist(FileAPI.GetCachePath(value.ToString()));
        //}
        );

    [RelayCommand]
    public async Task Share()
    {
        var tempName = "file.wav";
        var binaryData = FileAPI.ReadCacheData(Source);
        await StorageAPI.ExportData(tempName, binaryData);
    }

    [RelayCommand]
    public async Task Play()
    {
        PlayingView = true;
        await _audioController.OpenFile(FileAPI.GetCachePath(Source));
        _audioController.Play();
        _audioController.AddEventHandler(new EventHandler(HandlePlayEnded));
    }

    void HandlePlayEnded(object sender, EventArgs e)
    {
        PlayingView = false;
    }

    [RelayCommand]
    public void PauseResume()
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

    [RelayCommand]
    public void Stop()
    {
        PlayingView = false;
        _audioController.Stop();
    }
}