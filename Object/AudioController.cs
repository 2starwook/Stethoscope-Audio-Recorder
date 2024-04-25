using System.Diagnostics;
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
    private string filePath;

    public void OpenFile(string filePath)
	{
        try
		{
			if (currentAudioPlayer == null || this.filePath != filePath)
			{
				using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					this.currentAudioPlayer = audioManager.CreatePlayer(stream);
					this.filePath = filePath;
				}
            }
		}
		catch (Exception e)
		{
			// File doesn't exist
			Debug.WriteLine(e);
		}
    }

    public void OpenWithStream(Stream audioStream)
    {
        try
        {
            this.currentAudioPlayer = audioManager.CreatePlayer(audioStream);
        }
        catch (Exception e)
        {
			// File doesn't exist
			Debug.WriteLine(e);
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

	public double CurrentPosition()
	{
		return this.currentAudioPlayer?.CurrentPosition ?? 0.0;
	}

	public double Duration()
	{
		return this.currentAudioPlayer?.Duration ?? 1.0;
	}

	public double Volume()
	{
		return this.currentAudioPlayer?.Volume ?? 1;
	}

	public void SetVolume(double newVolume)
	{
		if (0 > newVolume || newVolume > 1)
		{
			throw new Exception($"Invalid newVolume {newVolume}");
		}
		if (currentAudioPlayer != null)
		{
			this.currentAudioPlayer.Volume = newVolume;
		}
	}

	public void Seek(double position)
	{
		this.currentAudioPlayer.Seek(position);
	}

	public void Dispose()
	{
		this.currentAudioPlayer?.Dispose();
	}
}