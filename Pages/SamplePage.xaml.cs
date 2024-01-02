using System;
using Microcharts;
using SkiaSharp;
using static Config;

namespace NET_MAUI_BLE.Pages;

public partial class SamplePage : ContentPage {
	bool isScanning = true;
    // flag that shows if scanner is currently scanning or not
    string waitingString = "Waiting to be connected...";
    // serviceUUID_16 = "19B10000-E8F2-537E-4F6C-D104768A1214".ToLower();

	public SamplePage() {
		InitializeComponent();
        resultData.Text = waitingString;
		BindingContext = this; // Bind modified data with xaml file
	}

	private void OnScanControllerClicked(object sender, EventArgs e) {
		if (isScanning == true) { // Stop scanning
			isScanning = false;
			ScanControllerBtn.Text = $"Start Scan";
            resultData.Text = waitingString;
		}
		else { // Start scanning
			isScanning = true;
			ScanControllerBtn.Text = $"Stop Scan";
		}
		SemanticScreenReader.Announce(ScanControllerBtn.Text);
	}

}

