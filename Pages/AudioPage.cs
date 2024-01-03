using System;
using System.Diagnostics;
using Plugin.Maui.Audio;
using Object.Audio;

using static Config;


namespace NET_MAUI_BLE.Pages;

public partial class AudioPage : ContentPage {
	private AudioController audioController;
	private IAudioPlayer current_player;
	string filename = "mysound.wav";

	public AudioPage(IAudioManager audioManager) {
		InitializeComponent();
		this.audioController = new AudioController(audioManager);
	}

	private async void PlayBtnClicked(object sender, EventArgs e)
	{
		if (this.current_player == null){
			this.current_player = await this.audioController.PlaySound(filename);
			this.current_player.PlaybackEnded += new EventHandler(HandlePlayEnded);
		}

		if (this.audioController.IsPlaying(this.current_player)){
			audioController.Stop(this.current_player);
			PlayBtn.Text = "Play";
		}
		else {
			audioController.Play(this.current_player);
			PlayBtn.Text = "Stop";
		}
    }

	void HandlePlayEnded(object sender, EventArgs e){
		PlayBtn.Text = "Play";
	}

}

