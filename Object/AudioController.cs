using System;
using System.Diagnostics;
using Plugin.Maui.Audio;

namespace Object.Audio;
public class AudioController
{
	private readonly IAudioManager audioManager;

	public AudioController(IAudioManager audioManager) {
		this.audioManager = audioManager;
	}

    public async Task<IAudioPlayer> PlaySound(string filename){
        IAudioPlayer player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(filename));
        return player;
    }

	public void Play(IAudioPlayer player){
		player.Play();
	}

	public void Stop(IAudioPlayer player){
		player.Stop();
	}

	public bool IsPlaying(IAudioPlayer player){
		return player.IsPlaying;
	}

}
