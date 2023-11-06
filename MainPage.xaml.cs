using System;
using Shiny;
using Shiny.BluetoothLE;
using System.Collections;
using BluetoothCourse.Extensions;
using System.Collections.ObjectModel;

namespace NET_MAUI_BLE;

public partial class MainPage : ContentPage
{
	private readonly IBleManager _bleManager;
	public ObservableList<ScanResult> Results { get; } = new ObservableList<ScanResult>();
	bool isScanning = true;

	// Practice Purpose
	public Collection<String> DebuggerCollection { get; set; } = new Collection<string>();

	public MainPage(IBleManager bleManager)
	{
		_bleManager = bleManager;
		InitializeComponent();
		// Title.Text = "Started";
		DebuggerCollection.Add("Apple");

		BindingContext = this; // Bind modified data with xaml file
		System.Diagnostics.Debug.WriteLine("MainPage Constructor ended");
	}

	private void OnScanControllerClicked(object sender, EventArgs e)
	{
		if (isScanning == true){
			_bleManager.StopScan();
			isScanning = false;
			ScanControllerBtn.Text = $"Start Scan";
		}
		else{
			Scan();
			isScanning = true;
			ScanControllerBtn.Text = $"Stop Scan";
		}

		SemanticScreenReader.Announce(ScanControllerBtn.Text);
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        try { Scan(); }
        catch { }

        lvDevices.ItemSelected += async (s, e) => {
            System.Diagnostics.Debug.WriteLine($"Item Selected: {lvDevices.SelectedItem.GetType().ToString()}");

            var device = (ScanResult)lvDevices.SelectedItem;
            
            device.Peripheral.WhenStatusChanged().Subscribe(_status => {
                System.Diagnostics.Debug.WriteLine($"Status Changed: ${_status.ToString()}");
            });

            await device.Peripheral.ConnectAsync();
            device.Peripheral.CancelConnection();
        };
    }

    private async Task<int> MeasureHeartRate(IPeripheral device)
    {
        TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

        await device.ConnectAsync();

        var serviceUUID = 0x180D.UuidFromPartial();
        var characteristicsUUID = 0x2A37.UuidFromPartial();
        var characteristic = await device.GetCharacteristicAsync(serviceUUID.ToString(), characteristicsUUID.ToString());

        int counter = 0;
        int heartRate = 0;

        IDisposable notifications = null;

        notifications = device.NotifyCharacteristic(characteristic, true)
            .Subscribe(_result => {
                counter++;

                var data = _result.Data;

                if (data != null && data.Length > 0)
                {
                    var heartRateData = data.DecodeHeartRate();

                    if (counter == 0) 
                    {
                        heartRate = (int)heartRateData;
                    }
                    else
                    {
                        heartRate = (int)((heartRate + heartRateData) / 2);
                    }

                    if (counter == 10)
                    {
                        notifications.Dispose();
                        _ = tcs.TrySetResult(heartRate);
                        device.CancelConnection();
                    }
                }
            });

        return await tcs.Task;
    }

    public void Scan()
    {
        if (!_bleManager.IsScanning)
        {
            _bleManager.StopScan();
        }

        Results.Clear();

        var heartRateServiceUuid = "180d";

        _bleManager.Scan()
            .Subscribe(async _scanResult => {
                // System.Diagnostics.Debug.WriteLine($"Scanned for: {scanResult.Peripheral.Uuid.ToString()}");
                if (_scanResult.AdvertisementData != null && _scanResult.AdvertisementData.ServiceUuids != null){
                    if (_scanResult.AdvertisementData.ServiceUuids.Contains(heartRateServiceUuid.ToString())){
                        var scannedData = await MeasureHeartRate(_scanResult.Peripheral);
                        System.Diagnostics.Debug.WriteLine($"{scannedData.ToString()}");
                    }
                }
                if (!Results.Any(a => a.Peripheral.Uuid.Equals(_scanResult.Peripheral.Uuid)))
                {
                    Results.Add(_scanResult);
                }
                
            });
    }


}

