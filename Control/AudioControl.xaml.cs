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
        dispatcher = Application.Current.Dispatcher;
        PlayingView = false;
        PauseResumeText = "Pause";
        Loaded += OnLoading;
        Unloaded += OnUnloading;
        InitializeComponent();
    }

    private AudioController _audioController;
    private readonly IDispatcher dispatcher;
    private bool _playingView;
    private string _pauseResumeText;
    private bool isPositionChangeSystemDriven;
    private bool isDisposed;
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
    public bool PlayingView {
        get => _playingView;
        set
        {
            _playingView = value;
            OnPropertyChanged(nameof(PlayingView));
        }
    }
    public string PauseResumeText {
        get => _pauseResumeText;
        set
        {
            _pauseResumeText = value;
            OnPropertyChanged(nameof(PauseResumeText));
        }
    }
    public double CurrentPosition
    {
        get => _audioController.CurrentPosition();
        set
        {
            if (_audioController is not null &&
                isPositionChangeSystemDriven is false)
            {
                _audioController.Seek(value);
            }
        }
    }
    public double Duration => _audioController.Duration();
    public double Volume
    {
        get => _audioController.Volume();
        set => _audioController.SetVolume(value);
    }


    private void OnLoading(object sender, EventArgs e)
    {
        _audioController.OpenFile(Source);
        OnPropertyChanged(nameof(Duration));
    }

    private void OnUnloading(object sender, EventArgs e)
    {
        Stop();
    }

    [RelayCommand]
    public async Task Share()
    {
        var tempName = "file.wav";
        var binaryData = FileAPI.ReadData(Source);
        await StorageAPI.ExportData(tempName, binaryData);
    }

    [RelayCommand]
    public void Play()
    {
        PlayingView = true;
        _audioController.Play();
        _audioController.AddEventHandler(new EventHandler(HandlePlayEnded));
        UpdatePlaybackPosition();
    }

    private void HandlePlayEnded(object sender, EventArgs e)
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
        UpdatePlaybackPosition();
    }

    [RelayCommand]
    public void Stop()
    {
        PlayingView = false;
        _audioController.Stop();
    }


    private void UpdatePlaybackPosition()
    {

        if (_audioController.IsPlaying() is false)
        {
            return;
        }

        dispatcher.DispatchDelayed(
            TimeSpan.FromMilliseconds(16),
            () =>
            {
                isPositionChangeSystemDriven = true;

                OnPropertyChanged(nameof(CurrentPosition));

                isPositionChangeSystemDriven = false;

                UpdatePlaybackPosition();
            });
    }

    public void TidyUp()
    {
        _audioController.Dispose();
        _audioController = null;
    }

    ~AudioControl()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (isDisposed)
        {
            return;
        }

        if (disposing)
        {
            TidyUp();
        }

        isDisposed = true;
    }
}