using Plugin.Maui.Audio;


namespace Object.MyAudio;
public class AudioPlayer
{
	private readonly IAudioPlayer _audioPlayer;
	private readonly string filePath;

	public AudioPlayer(IAudioPlayer audioPlayer, string filePath) {
		this._audioPlayer = audioPlayer;
		this.filePath = filePath;
	}

	public bool IsSameFilePath(string filePath){
		return this.filePath == filePath;
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
