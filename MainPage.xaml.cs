using System;
using Shiny;
using Shiny.BluetoothLE;
using System.Collections;
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
}

