using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiny.BluetoothLE;

using Object.MyMessage;
using Object.MyBLE;
using MyConfig;
using MyEnum;

namespace NET_MAUI_BLE.ViewModel;
// TODO - Implement receive audio file from BLE
public partial class HomeViewModel : ObservableObject, IRecipient<BleDataMessage>, IRecipient<BleStatusMessage>
{
	public HomeViewModel(IBleManager bleManager)
	{
        _bleController = new BleController(bleManager);
        WeakReferenceMessenger.Default.Register<BleDataMessage>(this);
        WeakReferenceMessenger.Default.Register<BleStatusMessage>(this);
        ResultText = "Waiting to be connected...";
        CurrentBleStatus = BleStatus.NotConnected;
        ScanView = true;
        ScanButtonView = true;

        CurrentRecordStatus = RecordStatus.Off;
        RecordView = false;
        RecordButtonView = true;
        PauseButtonView = false;
        ResumeButtonView = false;
    }

    private BleController _bleController;
    [ObservableProperty]
    private string resultText;
    [ObservableProperty]
    private string bleStatusText;

    [ObservableProperty]
    private bool scanView;
    [ObservableProperty]
    BleStatus currentBleStatus;
    [ObservableProperty]
    private bool scanButtonView;

    [ObservableProperty]
    private bool recordView;
    [ObservableProperty]
    RecordStatus currentRecordStatus;
    [ObservableProperty]
    private bool recordButtonView;
    [ObservableProperty]
    private bool pauseButtonView;
    [ObservableProperty]
    private bool resumeButtonView;

    [RelayCommand]
    void Appearing()
    {
        try
        {
            //ScanBLE();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }

    [RelayCommand]
    void ScanBLE()
    {
        _bleController.Scan();
        CurrentBleStatus = BleStatus.Scanning;
    }

    [RelayCommand]
    void StopBLE()
    {
        _bleController.StopScan();
        CurrentBleStatus = BleStatus.NotConnected;
    }

    partial void OnCurrentBleStatusChanged(BleStatus value)
    {
        if (value == BleStatus.Connected)
        {
            BleStatusText = "Connected";
            RecordView = true;
            ScanView = false;
        }
        else if (value == BleStatus.NotConnected)
        {
            BleStatusText = "Not Connected";
            ScanButtonView = true;
            RecordView = false;
            ScanView = true;
        }
        else if (value == BleStatus.Scanning)
        {
            BleStatusText = "Scanning";
            ScanButtonView = false;
        }
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

    partial void OnCurrentRecordStatusChanged(RecordStatus value)
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

    public void Receive(BleDataMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ResultText = "";
            foreach (var value in message.Value)
            {
                ResultText += $"{value.ToString()}\n";
            }
        });
    }

    public void Receive(BleStatusMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            BleStatusText = message.Value.ToString();
            CurrentBleStatus = message.Value;
        });
    }
}
