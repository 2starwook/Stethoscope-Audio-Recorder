using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shiny.BluetoothLE;

using MyConfig;

namespace NET_MAUI_BLE.ViewModel;

public partial class HomeViewModel : ObservableObject
{
	public HomeViewModel(IBleManager bleManager)
	{
        _bleManager = bleManager;
        ResultText = "Waiting to be connected...";
    }

    private readonly IBleManager _bleManager;
    private IPeripheral _connected_device;

    [ObservableProperty]
    string resultText;

    [RelayCommand]
    void Appearing()
    {
        try
        {
            Scan();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }

    private async Task<int> AnalyzeData(IPeripheral device)
    {
        TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

        // TODO - Need to handle when not get connected
        try
        {
            await device.ConnectAsync();
        }
        catch (TimeoutException e)
        {
            System.Diagnostics.Debug.WriteLine($"Timeout Exception occured {e.ToString()}");
            return await tcs.Task;
        }
        if (device.IsConnected())
        {
            _connected_device = device;
        }
#if DEBUG
        device.GetAllCharacteristics()
            .Subscribe(_result => {
                // Add breakpoint and wait. Then, check characteristic UUID once breaks
                foreach (var each in _result)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Service UUID: {each.Service.Uuid} / " +
                        $"Characteristic UUID: {each.Uuid}");
                }
            });
#endif
        device.GetCharacteristic(Config.SERVICE_UUTD, Config.CHARACTERISTIC_UUID)
            .Subscribe(characteristic => {
                device.ReadCharacteristic(characteristic)
                    .Subscribe(result => {
                        var data = result.Data;
                        System.Diagnostics.Debug.WriteLine($"Received Data: {data}");
                    });
            });

        return await tcs.Task;
    }

    public void Scan()
    {
        _bleManager.Scan()
            .Subscribe(async _scanResult => {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(
                    $@"Scanned for: {_scanResult.Peripheral.Uuid.ToString()} 
                     / {_scanResult.Peripheral.Name}");
#endif
                if (_scanResult.AdvertisementData != null &&
                _scanResult.AdvertisementData.ServiceUuids != null &&
                _scanResult.AdvertisementData.ServiceUuids.Contains(Config.SERVICE_UUTD))
                {
                    _bleManager.StopScan();
                    await AnalyzeData(_scanResult.Peripheral);
                }
            });
    }
}
