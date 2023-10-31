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

	// Practice Purpose
	int count = 0;
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

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
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
    public void Scan()
    {
        if (!_bleManager.IsScanning)
        {
            _bleManager.StopScan();
        }

        Results.Clear();

        var heartRateServiceUuid = 0x180D.UuidFromPartial();


        _bleManager.Scan()
            .Subscribe(scanResult => {
                System.Diagnostics.Debug.WriteLine($"Scanned for: {scanResult.Peripheral.Uuid.ToString()}");
							
                if (scanResult.AdvertisementData != null && 
                    scanResult.AdvertisementData.ServiceUuids != null &&
                    scanResult.AdvertisementData.ServiceUuids.Contains(heartRateServiceUuid.ToString())) {
                    if (!Results.Any(a => a.Peripheral.Uuid.Equals(scanResult.Peripheral.Uuid)))
                    {
                        Results.Add(scanResult);
                    }
                }
            });
    }


}

