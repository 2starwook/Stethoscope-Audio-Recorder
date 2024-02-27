using CommunityToolkit.Mvvm.Messaging;
using Shiny.BluetoothLE;
using Object.MyMessage;
using MyConfig;
using MyEnum;

namespace Object.MyBLE;
public class BleController
{
	public BleController(IBleManager bleManager)
    {
        _bleManager = bleManager;
        _service_uuid = Config.SERVICE_UUTD;
    }

    private readonly IBleManager _bleManager;
    private IPeripheral _connected_device;
    private string _service_uuid;


    public void Scan()
    {
        if (HasDevice())
        {
            Disconnect();
        }
        WeakReferenceMessenger.Default.Send(new BleStatusMessage(BleStatus.Scanning));
        _bleManager.Scan()
            .Subscribe(async _scanResult => {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(
                    $@"Scanned for: {_scanResult.Peripheral.Uuid.ToString()} 
                     / {_scanResult.Peripheral.Name}");
#endif
                if (_scanResult.AdvertisementData != null &&
                    _scanResult.AdvertisementData.ServiceUuids != null &&
                    _scanResult.AdvertisementData.ServiceUuids.Contains(_service_uuid))
                {
                    StopScan();
                    await Connect(_scanResult.Peripheral);
                    PrintAllCharacteristics();
                    // NOTE - Add needed handlers
                    HandleNotifyData(_service_uuid, Config.CHARACTERISTIC_UUID);
                }
            });
    }

    public void StopScan()
    {
        _bleManager.StopScan();
        if (_connected_device == null)
        {
            WeakReferenceMessenger.Default.Send(new BleStatusMessage(BleStatus.NotConnected));
        }
    }

    public async Task Connect(IPeripheral device)
	{
        _connected_device = device;
        try
        {
            await _connected_device.ConnectAsync();
        }
        catch (TimeoutException e)
        {
            System.Diagnostics.Debug.WriteLine($"Timeout Exception occured {e.ToString()}");
        }
        WeakReferenceMessenger.Default.Send(new BleStatusMessage(BleStatus.Connected));
    }

    public void Disconnect()
    {
        _connected_device.CancelConnection();
        _connected_device = null;
        WeakReferenceMessenger.Default.Send(new BleStatusMessage(BleStatus.NotConnected));
    }

    public bool HasDevice()
    {
        return _connected_device != null;
    }

    public void PrintAllCharacteristics()
    {
        _connected_device.GetAllCharacteristics()
            .Subscribe(_result => {
                foreach (var each in _result)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Service UUID: {each.Service.Uuid} / " +
                        $"Characteristic UUID: {each.Uuid}");
                }
            });
    }

    public void HandleReadData(string service_uuid, string characteristic_uuid)
    {
        _connected_device.GetCharacteristic(service_uuid, characteristic_uuid)
            .Subscribe(characteristic => {
                _connected_device.ReadCharacteristic(characteristic)
                    .Subscribe(result => {
                        var data = result.Data;
                        WeakReferenceMessenger.Default.Send(new BleDataMessage(data));
                        System.Diagnostics.Debug.WriteLine($"Read Data: {data}");
                    });
            });
    }

    public void HandleNotifyData(string service_uuid, string characteristic_uuid)
    {
        _connected_device.GetCharacteristic(service_uuid, characteristic_uuid)
            .Subscribe(characteristic => {
                _connected_device.NotifyCharacteristic(characteristic)
                    .Subscribe(result => {
                        var data = result.Data;
                        WeakReferenceMessenger.Default.Send(new BleDataMessage(data));
                        System.Diagnostics.Debug.WriteLine($"Notify Data: {data}");
                    });
            });
    }

    public void HandleWriteData(string service_uuid, string characteristic_uuid, byte[] data)
    {
        // NOTE - Need testing
        _connected_device.WriteCharacteristic(service_uuid, characteristic_uuid, data);
        System.Diagnostics.Debug.WriteLine($"Write Data: {data}");
    }
}
