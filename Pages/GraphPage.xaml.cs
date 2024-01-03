using System;
using System.Diagnostics;
using Microcharts;
using static Config;

namespace NET_MAUI_BLE.Pages;


public partial class GraphPage : ContentPage {

	public GraphPage() {
		InitializeComponent();
		try {
			chartView.Chart = new LineChart {
				Entries = entries
			};
		}
		catch (Exception e){
			System.Diagnostics.Debug.WriteLine(e.ToString());
		}
	}
	ChartEntry[] entries = new[]
	{
		new ChartEntry(212){
			Label = "Windows",
			ValueLabel = "112",
		},
		new ChartEntry(248){
			Label = "Android",
			ValueLabel = "648",
		},
		new ChartEntry(128){
			Label = "iOS",
			ValueLabel = "428",
		}
	};
}

