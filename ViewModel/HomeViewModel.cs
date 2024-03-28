using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NET_MAUI_BLE.Message.BleMessage;
using NET_MAUI_BLE.Object.BLE;
using MyEnum;


namespace NET_MAUI_BLE.ViewModel;

public partial class HomeViewModel : ObservableObject, IRecipient<BleDataMessage>, IRecipient<BleStatusMessage>
{
	public HomeViewModel()
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
    }
    public byte[] receivedFile = new byte[] {};

    private BleController _bleController;
    [ObservableProperty]
    private string resultText;
    [ObservableProperty]
    private string bleStatusText;

    [ObservableProperty]
    BleStatus currentBleStatus;
    [ObservableProperty]
    RecordStatus currentRecordStatus;
    [ObservableProperty]
    private bool recordView;
    [ObservableProperty]
    private bool recordButtonView;
    [ObservableProperty]
    private bool pauseButtonView;
    [ObservableProperty]
    private bool resumeButtonView;

#pragma warning disable CS1998
    [RelayCommand]
    async Task Appearing()
    {
        try
        {
            //NOTE - Remove comments if Ble needed
            //await InitiateBle();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }
#pragma warning restore CS1998

    [RelayCommand]
    async Task InitiateBle()
    {
        await _bleController.InitiateAsync();
    }

    partial void OnCurrentBleStatusChanged(BleStatus value)
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
            if (receivedFile.Length != 0)
            {
                // NOTE - Change in the future (Testing purpose)
                WriteFile("/Users/2star/Downloads/sample.wav");
            }
        }
    }

    private void WriteFile(string path)
    {
        File.WriteAllBytes(path, receivedFile);
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

    private void StoreData(byte[] data)
    {
        receivedFile = MYAPI.DataConvertAPI.Combine(receivedFile, data);
    }

    public void Receive(BleDataMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            StoreData(message.Value);
            foreach (var value in message.Value)
            {
                System.Diagnostics.Debug.Write($"{value.ToString()} ");
            }
            System.Diagnostics.Debug.Write("\n");
            System.Diagnostics.Debug.Write($"Received size: {receivedFile.Length}\n");
        });
    }

    public void Receive(BleStatusMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            CurrentBleStatus = message.Value;
        });
    }
}
