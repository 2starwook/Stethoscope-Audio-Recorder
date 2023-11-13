using System;
using Shiny;
using Shiny.BluetoothLE;
using BluetoothCourse.Extensions;

namespace NET_MAUI_BLE;

public partial class MainPage : ContentPage {
	private readonly IBleManager _bleManager;
	bool isScanning = true;
    // flag that shows if scanner is currently scanning or not

    string serviceUUID = "180d";
    string characteristicUUID = "2a37";
    string waitingString = "Waiting to be connected...";
    // serviceUUID_16 = "19B10000-E8F2-537E-4F6C-D104768A1214".ToLower();

	public MainPage(IBleManager bleManager) {
		_bleManager = bleManager;
		InitializeComponent();
        resultData.Text = waitingString;
		BindingContext = this; // Bind modified data with xaml file
	}

	private void OnScanControllerClicked(object sender, EventArgs e) {
		if (isScanning == true) { // Stop scanning
			_bleManager.StopScan();
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

        // // [Debuggin Purpose]
        // device.GetAllCharacteristics().Subscribe(_result => {
        //     // Add breakpoint and check serviceUUID
        //     var a = 1;
        // });
        device.GetCharacteristic(serviceUUID, characteristicUUID)
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
                // [Debugging Purpose]
                // System.Diagnostics.Debug.WriteLine(
                //     $@"Scanned for: {_scanResult.Peripheral.Uuid.ToString()} 
                //     / {_scanResult.Peripheral.Name}");
                
                if (_scanResult.AdvertisementData != null && 
                _scanResult.AdvertisementData.ServiceUuids != null &&
                _scanResult.AdvertisementData.ServiceUuids.Contains(serviceUUID)){
                    _bleManager.StopScan();
                    await AnalyzeData(_scanResult.Peripheral);
                }
            });
    }


}

