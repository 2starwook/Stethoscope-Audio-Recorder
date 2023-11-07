using System;
using Shiny;
using Shiny.BluetoothLE;
using System.Collections;
using BluetoothCourse.Extensions;
using System.Collections.ObjectModel;

namespace NET_MAUI_BLE;

public partial class MainPage : ContentPage {
	private readonly IBleManager _bleManager;
	bool isScanning = true;
    // flag that shows if scanner is currently scanning or not

	// Practice Purpose
	public Collection<String> DebuggerCollection { get; set; } = new Collection<string>();

	public MainPage(IBleManager bleManager) {
		_bleManager = bleManager;
		InitializeComponent();
		// DebuggerCollection.Add("Apple");

		BindingContext = this; // Bind modified data with xaml file
		System.Diagnostics.Debug.WriteLine("MainPage Constructor ended");
	}

	private void OnScanControllerClicked(object sender, EventArgs e) {
		if (isScanning == true) {
			_bleManager.StopScan();
			isScanning = false;
			ScanControllerBtn.Text = $"Start Scan";
		}
		else {
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

        await device.ConnectAsync();

        var serviceUUID_128 = 0x180D.UuidFromPartial();
        var characteristicsUUID_128 = 0x2A37.UuidFromPartial();
        var characteristic = await device.GetCharacteristicAsync(serviceUUID_128.ToString(), characteristicsUUID_128.ToString());

        int counter = 0;
        int shownData = 0;

        IDisposable notifications = null;

        notifications = device.NotifyCharacteristic(characteristic, true)
            .Subscribe(_result => {
                counter++;

                var data = _result.Data;

                if (data != null && data.Length > 0) {
                    var ScannedData = data.DecodeHeartRate();

                    if (counter == 0) {
                        shownData = (int)ScannedData;
                    }
                    else {
                        shownData = (int)((shownData + ScannedData) / 2);
                    }

                    if (counter == 10) {
                        //Show average data
                        notifications.Dispose();
                        _ = tcs.TrySetResult(shownData);
                        device.CancelConnection();
                    }
                }
            });

        return await tcs.Task;
    }

    public void Scan() {
        if (!_bleManager.IsScanning) {
            _bleManager.StopScan();
        }

        var serviceUDID_16 = "180d";

        _bleManager.Scan()
            .Subscribe(async _scanResult => {
                // System.Diagnostics.Debug.WriteLine($"Scanned for: {scanResult.Peripheral.Uuid.ToString()}");
                if (_scanResult.AdvertisementData != null && _scanResult.AdvertisementData.ServiceUuids != null) {
                    if (_scanResult.AdvertisementData.ServiceUuids.Contains(serviceUDID_16.ToString())) {
                        var scannedData = await AnalyzeData(_scanResult.Peripheral);
                        resultData.Text = scannedData.ToString();
                    }
                }
            });
    }


}

