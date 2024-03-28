using Plugin.Maui.Audio;


namespace NET_MAUI_BLE.Object.Audio;

public class AudioPlayer
{
	public AudioPlayer(IAudioPlayer audioPlayer, string filePath) {
		this._audioPlayer = audioPlayer;
		this._filePath = filePath;
	}

	private readonly IAudioPlayer _audioPlayer;
	private readonly string _filePath;

	public bool IsSameFilePath(string filePath){
		return this._filePath == filePath;
	}

	public void Play(){
		_audioPlayer.Play();
	}

	public void Stop(){
		_audioPlayer.Stop();
	}

	public void Pause(){
		_audioPlayer.Pause();
	}

	public bool IsPlaying(){
		return _audioPlayer.IsPlaying;
	}

	public void AddEventHandler(EventHandler e){
		_audioPlayer.PlaybackEnded += e;
	}
}
