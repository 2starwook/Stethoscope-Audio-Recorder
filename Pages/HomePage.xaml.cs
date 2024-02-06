using System;
using Shiny;
using Shiny.BluetoothLE;
using BluetoothCourse.Extensions;

using MyConfig;

namespace NET_MAUI_BLE.Pages;

public partial class HomePage : ContentPage {
	private readonly IBleManager _bleManager;
    private IPeripheral _connected_device;
	bool isScanning = true;
    // flag that shows if scanner is currently scanning or not

    string waitingString = "Waiting to be connected...";

	public HomePage(IBleManager bleManager) {
		_bleManager = bleManager;
		InitializeComponent();
        resultData.Text = waitingString;
		BindingContext = this; // Bind modified data with xaml file
	}

	private void OnScanControllerClicked(object sender, EventArgs e) {
		if (isScanning == true) { // Stop scanning
			_bleManager.StopScan();
            if (_connected_device != null)
            {
                _connected_device.CancelConnection();
                _connected_device = null;
            }
			isScanning = false;
			ScanControllerBtn.Text = $"Start Scan";
            resultData.Text = waitingString;
		}
		else { // Start scanning
			Scan();
			isScanning = true;
			ScanControllerBtn.Text = $"Stop Scan";
		}
		SemanticScreenReader.Announce(ScanControllerBtn.Text);
	}

    protected override void OnAppearing() {
        base.OnAppearing();

        try { Scan(); }
        catch { }
    }

    private async Task<int> AnalyzeData(IPeripheral device) {
        TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

        // TODO - Need to handle when not get connected
        try{
            await device.ConnectAsync();
        }
        catch (TimeoutException e) {
            System.Diagnostics.Debug.WriteLine($"Timeout Exception occured {e.ToString()}");
            return await tcs.Task;
        }
        if (device.IsConnected())
        {
            _connected_device = device;
        }
        #if DEBUG
        device.GetAllCharacteristics().Subscribe(_result => {
            // Add breakpoint and wait. Then, check characteristic UUID once breaks
            foreach(var each in _result)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Service UUID: {each.Service.Uuid} / " +
                    $"Characteristic UUID: {each.Uuid}");
            }
        });
        #endif
        device.GetCharacteristic(Config.SERVICE_UUTD, Config.CHARACTERISTIC_UUID)
            .Subscribe(characteristic => {
                device.NotifyCharacteristic(characteristic, true)
                    .Subscribe(notification => {
                        var data = notification.Data;
                        if (data != null && data.Length > 0) {
                            resultData.Text = data.DecodeHeartRate().ToString();
                        }
                    });
            });

        return await tcs.Task;
    }

    public void Scan() {
        _bleManager.Scan()
            .Subscribe(async _scanResult => {
                #if DEBUG
                System.Diagnostics.Debug.WriteLine(
                    $@"Scanned for: {_scanResult.Peripheral.Uuid.ToString()} 
                     / {_scanResult.Peripheral.Name}");
                #endif
                if (_scanResult.AdvertisementData != null && 
                _scanResult.AdvertisementData.ServiceUuids != null &&
                _scanResult.AdvertisementData.ServiceUuids.Contains(Config.SERVICE_UUTD)){
                    _bleManager.StopScan();
                    await AnalyzeData(_scanResult.Peripheral);
                }
            });
    }
}

