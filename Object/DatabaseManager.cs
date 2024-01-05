using Object.MyAudio;
using MyAPI;
using MyConfig;


namespace Object.MyData;
public class DatabaseManager {
    // Manage Database: Add/Remove/Modify data
    private RecordCollection recordCollection;
    
	public DatabaseManager() {
        if(!File.Exists(Config.dataDirPath)){
            FileAPI.CreateDirectory(Config.dataDirPath);
        }
		var files = FileAPI.GetFiles(Config.dataDirPath);
        this.recordCollection = new RecordCollection(files);
	}

    public void AddData(string audioFilePath){
        recordCollection.AddRecord(audioFilePath);
    }

    public void DeleteData(string audioFilePath){
        recordCollection.DeleteRecord(audioFilePath);
        FileAPI.DeleteFile(audioFilePath);
    }

    public void AttachPatientToRecord(string audioFilePath, string patientId){
        // TODO
    }

    public void DeleteAssignedPatientFromRecord(string audioFilePath){
        // TODO
    }

    public bool IsDataExist(string audioFilePath){
        return recordCollection.IsExist(audioFilePath);
    }

    public async void ImportAudioFile(){
        var srcPath = await StorageAPI.GetFilePath();
        var filename = GetUniqueID() + ".wav";
        var dstPath = Path.Combine(Config.dataDirPath, filename);
        File.Copy(srcPath.ToString(), dstPath);
        AddData(dstPath);
    }

    public List<string> GetPathList(){
        return recordCollection.GetPathList();
    }

    private static string GetUniqueID(){
        return Guid.NewGuid().ToString().ToUpper();
    }

}
