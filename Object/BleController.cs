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
        _characteristic_uuid = Config.CHARACTERISTIC_UUID;
        SubscribeToMessenger();
    }
    private string _device_guid;
    private string _service_uuid;
    private string _characteristic_uuid;
    private IAdapter _adapter;


    public async Task InitiateAsync()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"Attempt to connect BLE");
            IDevice targetDevice = await ConnectAsync();
            System.Diagnostics.Debug.WriteLine($"BLE Connected");
            if (targetDevice != null)
            {
                //await PrintAllServicesAsync(targetDevice);
                //await PrintAllCharacteristicsAsync(targetDevice, _service_uuid);
                System.Diagnostics.Debug.WriteLine($"Attempt to get characteristic");
                ICharacteristic characteristic = await GetCharacteristicAsync(
                    targetDevice, _service_uuid, _characteristic_uuid);
                System.Diagnostics.Debug.WriteLine($"Attempt to handle notify data");
                await HandleNotifyDataAsync(characteristic);
                SendBleStatusMessage(BleStatus.Connected);
            }
        }
        catch(Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e}");
        }
    }

    private async Task<IDevice> ConnectAsync()
	{
        SendBleStatusMessage(BleStatus.Scanning);
        return await _adapter.ConnectToKnownDeviceAsync(new Guid(_device_guid));
    }

    private async Task<ICharacteristic> GetCharacteristicAsync(IDevice target_device,
        string service_uuid, string characteristic_uuid)
    {
        var service = await target_device.GetServiceAsync(new Guid(service_uuid));
        return await service.GetCharacteristicAsync(new Guid(characteristic_uuid));
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

    private void HandleReadData(ICharacteristic characteristic)
    {

    }

    private void HandleWriteData(ICharacteristic characteristic, byte[] data)
    {

    }

    private void SubscribeToMessenger()
    {
        _adapter.DeviceConnected += (s, device) => {
            //Needs to get service/characteristics
            SendBleStatusMessage(BleStatus.Connecting);
        };
        _adapter.DeviceDisconnected += (s,e) => {
            SendBleStatusMessage(BleStatus.Disconnected);
        };
        _adapter.DeviceConnectionLost += (s,e) => {
            SendBleStatusMessage(BleStatus.Disconnected);
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
