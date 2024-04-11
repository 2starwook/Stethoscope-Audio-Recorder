using Plugin.Maui.Audio;


namespace NET_MAUI_BLE.Object.Audio;

public class AudioController
{
	public AudioController(IAudioManager audioManager)
	{
		this.audioManager = audioManager;
		//TODO - Convert to Async
	}

	private readonly IAudioManager audioManager;
	private IAudioPlayer currentAudioPlayer;
    private readonly string filePath;

    public async Task OpenFile(string filePath)
	{
        try
		{
			if (currentAudioPlayer == null || this.filePath != filePath)
			{
				this.currentAudioPlayer = audioManager.CreatePlayer(
						await FileSystem.OpenAppPackageFileAsync(filePath));
            }
		}
		catch
		{
			// File doesn't exist
		}
    }

    public void OpenWithStream(Stream audioStream)
    {
        try
        {

            this.currentAudioPlayer = audioManager.CreatePlayer(audioStream);
        }
        catch
        {
            // File doesn't exist
        }
    }

    public void AddEventHandler(EventHandler e)
	{
        this.currentAudioPlayer.PlaybackEnded += e;
	}

	public bool HasAudioPlayer()
	{
        return this.currentAudioPlayer != null;
	}

	public void Play()
	{
		this.currentAudioPlayer.Play();
    }

	public void Stop()
	{
		this.currentAudioPlayer.Stop();
	}

	public void Pause()
	{
		this.currentAudioPlayer.Pause();
	}

	public bool IsPlaying()
	{
		return this.currentAudioPlayer.IsPlaying;
	}
}