using Plugin.Maui.Audio;


namespace Object.MyAudio;
public class AudioController
{
	private readonly IAudioManager audioManager;
	private AudioPlayer _currentAudioPlayer;

	public AudioController(IAudioManager audioManager) {
		this.audioManager = audioManager;
	}

    public async void OpenFile(string filename){
		try {
        	this._currentAudioPlayer = new AudioPlayer(
				audioManager.CreatePlayer(
					await FileSystem.OpenAppPackageFileAsync(filename)));
		}
		catch {
			// File doesn't exist
		}
    }

	public void AddEventHandler(EventHandler e){
		this._currentAudioPlayer.AddEventHandler(e);
	}

	public bool HasAudioPlayer(){
		return this._currentAudioPlayer != null;
	}

	public void Play(){
		this._currentAudioPlayer.Play();
	}

	public void Stop(){
		this._currentAudioPlayer.Stop();
	}

	public void Pause(){
		this._currentAudioPlayer.Pause();
	}

	public bool IsPlaying(){
		return this._currentAudioPlayer.IsPlaying();
	}

}
