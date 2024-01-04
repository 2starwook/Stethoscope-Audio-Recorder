using System.IO;
using System.Diagnostics;
using System.Globalization;
using CommunityToolkit.Maui.Storage;
using Microsoft.Maui.Media;

using static Config;
using Object.MyFileController;
using Object.MyAudio;
using Plugin.Maui.Audio;


namespace NET_MAUI_BLE.Pages;

public partial class TestPage : ContentPage {
	private AudioController audioController;
	private FileController fileController;

	public TestPage(IAudioManager audioManager) {
		InitializeComponent();
		this.audioController = new AudioController(audioManager);
		this.fileController = new FileController();
	}

	private async void Btn1Clicked(object sender, EventArgs e) {
		try {
			var path = await this.fileController.GetDeviceFilePath();
			if (path != null)
			{
				label.Text = path;
			}
		}
		catch {
			// Exception thrown when user cancels
		}
    }
	private async void Btn2Clicked(object sender, EventArgs e) {
		try {
			audioController.OpenFile(label.Text);
			if (audioController.IsPlaying()) {
				audioController.Stop();
			}
			else {
				audioController.Play();
			}

		}
		catch {
			// Exception thrown when user cancels
		}
    }

}

