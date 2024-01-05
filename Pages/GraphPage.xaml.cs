﻿using System;
using System.Diagnostics;
using Microcharts;
using SkiaSharp;
using MyConfig;

namespace NET_MAUI_BLE.Pages;


public partial class GraphPage : ContentPage {
	ChartEntry[] data = new[]
	{
			// Label = "112",
			// ValueLabel = "112",
		new ChartEntry(-100){},
		new ChartEntry(100){},
		new ChartEntry(-100){},
		new ChartEntry(100){},
		new ChartEntry(-100){},
		new ChartEntry(100){},
		new ChartEntry(-100){},
		new ChartEntry(100){},
		new ChartEntry(-100){},
		new ChartEntry(100){},
		new ChartEntry(-100){},
		new ChartEntry(100){},
		new ChartEntry(-100){},
		new ChartEntry(100){},
		new ChartEntry(-100){},
	};

	public GraphPage() {
		// TODO - How to Convert audio to number
		InitializeComponent();
		try {
			chartView.Chart = new LineChart {
				Entries = data,
				IsAnimated = false,
				LineAreaAlpha = 0, // Remove background grey
			};
		}
		catch (Exception e){
			System.Diagnostics.Debug.WriteLine(e.ToString());
		}
	}
}

