using System;
using System.Diagnostics;
using Microcharts;
// using SkiaSharp;
using static Config;

namespace NET_MAUI_BLE.Pages;

public partial class TestPage : ContentPage {
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

	public TestPage() {
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

}

