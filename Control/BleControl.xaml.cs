using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using NET_MAUI_BLE.Message.BleMessage;
using NET_MAUI_BLE.Object.BLE;
using NET_MAUI_BLE.Enum;


namespace NET_MAUI_BLE.Controls;

public partial class BleControl : ContentView, IRecipient<BleDataMessage>, IRecipient<BleStatusMessage>
{
    public BleControl()
    {
        _bleController = new BleController();
        WeakReferenceMessenger.Default.Register<BleDataMessage>(this);
        WeakReferenceMessenger.Default.Register<BleStatusMessage>(this);
        ResultText = "Waiting to be connected...";
        BleStatusText = "Scanning";

        CurrentBleStatus = BleStatus.Scanning;

        CurrentRecordStatus = RecordStatus.Off;
        RecordView = false;
        RecordButtonView = true;
        PauseButtonView = false;
        ResumeButtonView = false;
        InitializeComponent();
    }
    private BleController _bleController;
    private string resultText;
    public string ResultText
    {
        get => resultText;
        set
        {
            resultText = value;
            OnPropertyChanged(nameof(ResultText));
        }
    }

    private string bleStatusText;
    public string BleStatusText
    {
        get => bleStatusText;
        set
        {
            bleStatusText = value;
            OnPropertyChanged(nameof(BleStatusText));
        }
    }

    private BleStatus currentBleStatus;
    public BleStatus CurrentBleStatus
    {
        get => currentBleStatus;
        set
        {
            currentBleStatus = value;
            OnCurrentBleStatusChanged(value);
            OnPropertyChanged(nameof(CurrentBleStatus));
        }
    }

    private RecordStatus currentRecordStatus;
    public RecordStatus CurrentRecordStatus
    {
        get => currentRecordStatus;
        set
        {
            currentRecordStatus = value;
            OnCurrentRecordStatusChanged(value);
            OnPropertyChanged(nameof(CurrentRecordStatus));
        }
    }

    private bool recordView;
    public bool RecordView
    {
        get => recordView;
        set
        {
            recordView = value;
            OnPropertyChanged(nameof(RecordView));
        }
    }

    
    private bool recordButtonView;
    public bool RecordButtonView
    {
        get => recordButtonView;
        set
        {
            recordButtonView = value;
            OnPropertyChanged(nameof(RecordButtonView));
        }
    }


    private bool pauseButtonView;
    public bool PauseButtonView
    {
        get => pauseButtonView;
        set
        {
            pauseButtonView = value;
            OnPropertyChanged(nameof(PauseButtonView));
        }
    }

    private bool resumeButtonView;
    public bool ResumeButtonView
    {
        get => resumeButtonView;
        set
        {
            resumeButtonView = value;
            OnPropertyChanged(nameof(ResumeButtonView));
        }
    }

    private void OnCurrentBleStatusChanged(BleStatus value)
    {
        if (value == BleStatus.Scanning)
        {
            BleStatusText = "Scanning";
        }
        else if (value == BleStatus.Connecting)
        {
            BleStatusText = "Connecting";
        }
        else if (value == BleStatus.Connected)
        {
            BleStatusText = "Connected";
            RecordView = true;
        }
        else if (value == BleStatus.Disconnected)
        {
            BleStatusText = "Disconnected";
            RecordView = false;
        }
    }

    private void OnCurrentRecordStatusChanged(RecordStatus value)
    {
        if (value == RecordStatus.Off)
        {
            RecordView = false;
        }
        else if (value == RecordStatus.Recording)
        {
            RecordButtonView = false;
            PauseButtonView = true;
            ResumeButtonView = false;
        }
        else if (value == RecordStatus.NotRecording)
        {
            RecordButtonView = true;
            PauseButtonView = false;
            ResumeButtonView = false;
        }
        else if (value == RecordStatus.Paused)
        {
            RecordButtonView = false;
            PauseButtonView = false;
            ResumeButtonView = true;
        }
    }

    [RelayCommand]
    async Task InitiateBle()
    {
        await _bleController.InitiateAsync();
    }

    [RelayCommand]
    void Record()
    {
        if (CurrentRecordStatus == RecordStatus.Paused)
        {
            // Resume
        }
        CurrentRecordStatus = RecordStatus.Recording;
    }

    [RelayCommand]
    void Stop()
    {
        CurrentRecordStatus = RecordStatus.NotRecording;
    }

    [RelayCommand]
    void Pause()
    {
        CurrentRecordStatus = RecordStatus.Paused;
    }

    public void Receive(BleDataMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var value in message.Value)
            {
                System.Diagnostics.Debug.Write($"{value} ");
            }
            System.Diagnostics.Debug.Write("\n");
        });
    }

    public void Receive(BleStatusMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            CurrentBleStatus = message.Value;
        });
    }

    //public string Source
    //{
    //    get => GetValue(SourceProperty) as string;
    //    set => SetValue(SourceProperty, value);
    //}

    //public static readonly BindableProperty SourceProperty = BindableProperty.Create(
    //    nameof(Source), typeof(string), typeof(AudioControl),
    //    propertyChanged: (bindable, oldValue, newValue) =>
    //    {
    //        var control = (AudioControl)bindable;
    //    }
    //    //validateValue: (bindable, value) =>
    //    //{
    //    //    return FileAPI.isExist(FileAPI.GetCachePath(value.ToString()));
    //    //}
    //    );
}