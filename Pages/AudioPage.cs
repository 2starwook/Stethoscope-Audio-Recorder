using System;
using System.Diagnostics;
using Plugin.Maui.Audio;

using static Config;


namespace NET_MAUI_BLE.Pages;

public partial class AudioPage : ContentPage {
	private readonly IAudioManager audioManager;
	private IAudioPlayer player;
	string filename = "mysound.wav";

	public AudioPage(IAudioManager audioManager) {
		InitializeComponent();
		this.audioManager = audioManager;
	}

	private async void PlayBtnClicked(object sender, EventArgs e)
	{
		if (this.player == null){
			this.player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(filename));
		}

		if (this.player.IsPlaying){
			Stop();
		}
		else {
			Play();
		}
		this.player.PlaybackEnded += new EventHandler(HandlePlayEnded);
    }

	private void Stop(){
		this.player.Stop();
		PlayBtn.Text = "Play";
	}

	private void Play(){
		this.player.Play();
		PlayBtn.Text = "Stop";
	}

	void HandlePlayEnded(object sender, EventArgs e){
		PlayBtn.Text = "Play";
	}

}

