using Plugin.Maui.Audio;

namespace Object.MyAudio;
public class AudioPlayer
{
	private readonly IAudioPlayer _audioPlayer;

	public AudioPlayer(IAudioPlayer audioPlayer) {
		this._audioPlayer = audioPlayer;
	}

	public void Play(){
		this._audioPlayer.Play();
	}

	public void Stop(){
		this._audioPlayer.Stop();
	}

	public void Pause(){
		this._audioPlayer.Pause();
	}

	public bool IsPlaying(){
		return this._audioPlayer.IsPlaying;
	}

	public void AddEventHandler(EventHandler e){
		this._audioPlayer.PlaybackEnded += e;
	}

}
