using System;
using System.Diagnostics;
using Plugin.Maui.Audio;
using Object.MyAudio;

using MyConfig;


namespace NET_MAUI_BLE.Pages;

public partial class AudioPage : ContentPage {
	private AudioController audioController;
	string filename = "mysound.wav";

	public AudioPage(IAudioManager audioManager) {
		InitializeComponent();
		this.audioController = new AudioController(audioManager);
	}

	//private async Task PlayBtnClicked(object sender, EventArgs e)
	//{
	//	if (!this.audioController.HasAudioPlayer()){
	//		await this.audioController.OpenFile(filename);
	//		this.audioController.AddEventHandler(new EventHandler(HandlePlayEnded));
	//	}

	//	if (this.audioController.IsPlaying()){
	//		audioController.Stop();
	//		PlayBtn.Text = "Play";
	//	}
	//	else {
	//		audioController.Play();
	//		PlayBtn.Text = "Stop";
	//	}
 //   }

	//void HandlePlayEnded(object sender, EventArgs e){
	//	PlayBtn.Text = "Play";
	//}

}

