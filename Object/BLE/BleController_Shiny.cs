using CommunityToolkit.Mvvm.Messaging;
using Shiny.BluetoothLE;
using Object.MyMessage;
using MyConfig;
using MyEnum;


namespace Object.MyBLE;
public class BleControllerShiny
{
	public BleControllerShiny(IBleManager bleManager)
    {
        _bleManager = bleManager;
        _service_uuid = Config.SERVICE_UUTD;
    }
    private readonly IBleManager _bleManager;
    private IPeripheral targetDevice;
    private string _service_uuid;
    private int notifySubscriptionCount;


    public void ScanAndConnect()
    {
        if (IsConnected())
        {
            return;
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
                    System.Diagnostics.Debug.WriteLine($"Matching device found");
                    StopScan();
                    targetDevice = _scanResult.Peripheral;
                    await Connect();
                }
            });
    }

    public void StopScan()
    {
        _bleManager.StopScan();
    }

    private async Task Connect()
	{
        WeakReferenceMessenger.Default.Send(new BleStatusMessage(BleStatus.Connecting));
        System.Diagnostics.Debug.WriteLine($"Attempt to connect BLE");
        notifySubscriptionCount = 0;
        await Task.Run(async () =>
        {
            try
            {
                await targetDevice.ConnectAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
                WeakReferenceMessenger.Default.Send(new BleStatusMessage(BleStatus.Disconnected));
            }
        }).ContinueWith((result) =>
        {
            if (IsConnected())
            {
                SubscribeToMessenger();
                // NOTE - Add needed handlers
                System.Diagnostics.Debug.WriteLine($"Handle notify data");
                HandleNotifyData(_service_uuid, Config.CHARACTERISTIC_UUID);
            }
        });
    }

    private bool IsConnected()
    {
        return targetDevice != null && targetDevice.IsConnected();
    }

    private void PrintAllCharacteristics()
    {
        targetDevice.GetAllCharacteristics()
            .Subscribe(_result => {
                foreach (var each in _result)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Service UUID: {each.Service.Uuid} / " +
                        $"Characteristic UUID: {each.Uuid}");
                }
            });
    }

    private void HandleReadData(string service_uuid, string characteristic_uuid)
    {
        targetDevice.GetCharacteristic(service_uuid, characteristic_uuid)
            .Subscribe(characteristic => {
                targetDevice.ReadCharacteristic(characteristic)
                    .Subscribe(result => {
                        var data = result.Data;
                        WeakReferenceMessenger.Default.Send(new BleDataMessage(data));
                        System.Diagnostics.Debug.WriteLine($"Read Data: {data}");
                    });
            });
    }

    private void HandleNotifyData(string service_uuid, string characteristic_uuid)
    {
        if (notifySubscriptionCount != 0)
        {
            return;
        }
        notifySubscriptionCount += 1;
        Thread.Sleep(5000); // Wait till ready
        targetDevice.GetCharacteristic(service_uuid, characteristic_uuid)
            .Subscribe(characteristic => {
                //System.Diagnostics.Debug.WriteLine($"Delay 3s");
                //Thread.Sleep(3000);
                targetDevice.NotifyCharacteristic(characteristic)
                    .Subscribe(result => {
                        var data = result.Data;
                        WeakReferenceMessenger.Default.Send(new BleDataMessage(data));
                        System.Diagnostics.Debug.WriteLine($"Notify Data: {data}");
                    });
            });
    }

    private void HandleWriteData(string service_uuid, string characteristic_uuid, byte[] data)
    {
        // NOTE - Need testing
        targetDevice.WriteCharacteristic(service_uuid, characteristic_uuid, data);
        System.Diagnostics.Debug.WriteLine($"Write Data: {data}");
    }

    private void SubscribeToMessenger()
    {
        targetDevice.WhenConnectionFailed()
            .Subscribe(_ =>
            {
                targetDevice = null;
                WeakReferenceMessenger.Default.Send(new BleStatusMessage(BleStatus.Disconnected));
            }
        );
        targetDevice.WhenConnected()
            .Subscribe(peripheral =>
            {
                WeakReferenceMessenger.Default.Send(new BleStatusMessage(BleStatus.Connected));
            }
        );
        targetDevice.WhenDisconnected()
            .Subscribe(peripheral =>
            {
                targetDevice = null;
                WeakReferenceMessenger.Default.Send(new BleStatusMessage(BleStatus.Disconnected));
            }
        );
    }
}
