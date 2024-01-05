

namespace Object.MyAudio;
public class AudioFile
{
    private string path;

	public AudioFile(string path) {
        this.path = path;
	}

    public string GetPath() {
        return this.path;
    }
}
