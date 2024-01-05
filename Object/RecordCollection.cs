

namespace Object.MyAudio;
public class RecordCollection {
    // Manage a set of audio files
    private Dictionary<string, Record> data;

	public RecordCollection(string [] paths) {
        data = new Dictionary<string, Record>();
        foreach (var path in paths) {
            AddAudioFile(path);
        }
	}

    public int GetLength(){
        return data.Count();
    }

    public void AddAudioFile(string path){
        if (!IsExist(path)){            
            var record = new Record(path);
            data.Add(path, record);
        }
    }

    public bool IsExist(string path){
        return data.ContainsKey(path);
    }

    public void RemoveAudioFile(string path){
        if (IsExist(path)){
            data.Remove(path);
        }
    }

    public List<string> GetPathList(){
        List<string> pathList = new List<string>();
        foreach( KeyValuePair<string, Record> element in data ) {
            pathList.Add(element.Key);
        }
        return pathList;
    }

}
