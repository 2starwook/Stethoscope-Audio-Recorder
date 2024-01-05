

using Object.MyPatient;

namespace Object.MyAudio;
public class RecordCollection {
    // Manage a set of audio files
    private Dictionary<string, Record> data;

	public RecordCollection(string [] paths) {
        data = new Dictionary<string, Record>();
        foreach (var path in paths) {
            AddRecord(path);
        }
	}

    private Record GetRecord(string path){
        return data[path];
    }

    public int GetLength(){
        return data.Count();
    }

    public bool IsExist(string path){
        return data.ContainsKey(path);
    }

    public bool AddRecord(string path){
        var isSuccessful = false;
        if (!IsExist(path)){
            var record = new Record(path);
            data.Add(path, record);
            isSuccessful = true;
        }
        return isSuccessful;
    }

    public bool DeleteRecord(string path){
        var isSuccessful = false;
        if (IsExist(path)){
            data.Remove(path);
            isSuccessful = true;
        }
        return isSuccessful;
    }

    public bool AssignPatientToRecord(string path, Patient patient){
        var isSuccessful = false;
        if (IsExist(path)){
            var record = GetRecord(path);
            record.AssignPatient(patient);
            isSuccessful = true;
        }
        return isSuccessful;
    }

    public bool DeleteAssignedPatientFromRecord(string path){
        var isSuccessful = false;
        if (IsExist(path)){
            var record = GetRecord(path);
            record.DeleteAssignedPatient();
            isSuccessful = true;
        }
        return isSuccessful;
    }

    public List<string> GetPathList(){
        List<string> pathList = new List<string>();
        foreach( KeyValuePair<string, Record> element in data ) {
            pathList.Add(element.Key);
        }
        return pathList;
    }

}
