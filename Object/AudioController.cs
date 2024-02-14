using Plugin.Maui.Audio;


namespace Object.MyAudio;
public class AudioController
{
	public AudioController(IAudioManager audioManager) {
		this._audioManager = audioManager;
	}

	private readonly IAudioManager _audioManager;
	private AudioPlayer _currentAudioPlayer;

    public async Task OpenFile(string filePath){
		try {
			if (_currentAudioPlayer == null || !_currentAudioPlayer.IsSameFilePath(filePath)){
				this._currentAudioPlayer = new AudioPlayer(
					_audioManager.CreatePlayer(
						await FileSystem.OpenAppPackageFileAsync(filePath)), filePath);
			}
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
