

namespace Object.MyAudio;
public class AudioFileCollection
{
    private Dictionary<string, AudioFile> audioFiles;

	public AudioFileCollection(string [] paths) {
        audioFiles = new Dictionary<string, AudioFile>();
        foreach (var path in paths) {
            AddAudioFile(path);
        }
	}

    public int GetLength(){
        return audioFiles.Count();
    }

    public void AddAudioFile(string path){
        if (!IsExist(path)){            
            var audioFile = new AudioFile(path);
            audioFiles.Add(path, audioFile);
        }
    }

    public bool IsExist(string path){
        return audioFiles.ContainsKey(path);
    }

    public void RemoveAudioFile(string path){
        if (IsExist(path)){
            audioFiles.Remove(path);
        }
    }

}
