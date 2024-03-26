using CommunityToolkit.Mvvm.Messaging;
using Object.MyMessage;
using MyConfig;
using MyEnum;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;


namespace Object.MyBLE;
public class BleController
{
	public BleController()
    {
        _adapter = CrossBluetoothLE.Current.Adapter;
        _device_guid = "756d6f74-9a74-29ca-41a3-d5e5ef0acf84";
        _service_uuid = Config.SERVICE_UUTD;
        _characteristic_notify_uuid = Config.CHARACTERISTIC_UUID;
        _characteristic_setup_uuid = "8e632a60-ff9d-4f75-8899-ca76b3b3dfec";
        SubscribeToMessenger();
        SubscribeForScan(_device_guid);
    }
    private string _device_guid;
    private string _service_uuid;
    private string _characteristic_notify_uuid;
    private string _characteristic_setup_uuid;
    #pragma warning disable CS0414
    #pragma warning restore CS0414
    private IAdapter _adapter;


    public async Task InitiateAsync()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"Attempt to connect BLE");
            await ScanAsync();
            IDevice targetDevice = await ConnectAsync(_device_guid);
            System.Diagnostics.Debug.WriteLine($"BLE Connected");
            if (targetDevice != null)
            {
                //await PrintAllServicesAsync(targetDevice);
                //await PrintAllCharacteristicsAsync(targetDevice, _service_uuid);
                System.Diagnostics.Debug.WriteLine($"Attempt to get characteristic");
                ICharacteristic characteristic_notify = await GetCharacteristicAsync(
                    targetDevice, _service_uuid, _characteristic_notify_uuid);
                ICharacteristic characteristic_setup = await GetCharacteristicAsync(
                    targetDevice, _service_uuid, _characteristic_setup_uuid);

                System.Diagnostics.Debug.WriteLine($"Attempt to handle data");
                if (characteristic_setup.CanWrite)
                {
                    await HandleWriteData(characteristic_setup, new byte[] { 0x1 });
                }
                if (characteristic_notify.CanUpdate)
                {
                    await HandleNotifyDataAsync(characteristic_notify);
                }
                SendBleStatusMessage(BleStatus.Connected);
            }
        }
        catch(Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e}");
        }
    }

    private async Task ScanAsync()
    {
        await _adapter.StartScanningForDevicesAsync();
    }

    private async Task<IDevice> ConnectAsync(string device_guid)
	{
        SendBleStatusMessage(BleStatus.Scanning);
        return await _adapter.ConnectToKnownDeviceAsync(new Guid(device_guid));
    }

    private async Task<ICharacteristic> GetCharacteristicAsync(IDevice target_device,
        string service_uuid, string characteristic_uuid)
    {
        ICharacteristic characteristic = null;
        var service = await target_device.GetServiceAsync(new Guid(service_uuid));
        await MainThread.InvokeOnMainThreadAsync(async () => {
            characteristic = await service.GetCharacteristicAsync(new Guid(characteristic_uuid));
        });
        return characteristic;
    }

    private async Task HandleNotifyDataAsync(ICharacteristic characteristic)
    {
        characteristic.ValueUpdated += (o, args) =>
        {
            var data = args.Characteristic.Value;
            WeakReferenceMessenger.Default.Send(new BleDataMessage(data));
            System.Diagnostics.Debug.WriteLine($"Notify Data: {data}");
        };
        await characteristic.StartUpdatesAsync();
    }

    private async Task<(byte[], int)> HandleReadData(ICharacteristic characteristic)
    {
        return await characteristic.ReadAsync();
    }

    private async Task HandleWriteData(ICharacteristic characteristic, byte[] data)
    {
        await characteristic.WriteAsync(data);
    }

    private void SubscribeToMessenger()
    {
        _adapter.DeviceConnected += (s, device) => {
            SendBleStatusMessage(BleStatus.Connecting);
        };
        _adapter.DeviceDisconnected += (s,e) => {
            SendBleStatusMessage(BleStatus.Disconnected);
        };
        _adapter.DeviceConnectionLost += (s,e) => {
            SendBleStatusMessage(BleStatus.Disconnected);
        };
    }

    private void SubscribeForScan(string device_guid)
    {
        _adapter.DeviceDiscovered += async (s, device) => {
            System.Diagnostics.Debug.WriteLine($"{device.Device.Name} / {device.Device.Id}");
            if (device.Device.Id.ToString() == device_guid)
            {
                await _adapter.StopScanningForDevicesAsync();
                System.Diagnostics.Debug.WriteLine($"Device Found");
            }
        };
    }

    private void SendBleStatusMessage(BleStatus value){
        WeakReferenceMessenger.Default.Send(new BleStatusMessage(value));
    }

    private async Task PrintAllServicesAsync(IDevice target_device)
    {
        var services = await target_device.GetServicesAsync();
        foreach (var service in services)
        {
            System.Diagnostics.Debug.WriteLine($"Service ID: {service.Id}");
        }
    }

    private async Task PrintAllCharacteristicsAsync(IDevice target_device,
        string service_uuid)
    {
        var service = await target_device.GetServiceAsync(new Guid(service_uuid));
        var characteristics = await service.GetCharacteristicsAsync();
        foreach (var characteristic in characteristics)
        {
            System.Diagnostics.Debug.WriteLine($"Characteristics ID: {characteristic.Id}");
        }
    }

}
