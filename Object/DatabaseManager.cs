using Object.MyAudio;
using MyAPI;
using MyConfig;


namespace Object.MyData;
public class DatabaseManager {
    // Manager Database: Add/Remove/Modify data
    private RecordCollection recordCollection;
    
	public DatabaseManager() {
        if(!File.Exists(Config.dataDirPath)){
            FileManager.CreateDirectory(Config.dataDirPath);
        }
		var files = FileManager.GetFiles(Config.dataDirPath);
        this.recordCollection = new RecordCollection(files);
	}

    public void AddData(string audioFilePath){
        recordCollection.AddRecord(audioFilePath);
    }

    public void DeleteData(string audioFilePath){
        recordCollection.DeleteRecord(audioFilePath);
        FileManager.DeleteFile(audioFilePath);
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

    public void ImportAudioFile(){
        var srcPath = StorageAPI.GetFilePath().ToString();
        var filename = GetUniqueID() + ".wav";
        var dstPath = Path.Combine(Config.dataDirPath, filename);
        File.Copy(srcPath, dstPath);
        AddData(dstPath);
    }

    public List<string> GetPathList(){
        return recordCollection.GetPathList();
    }

    private static string GetUniqueID(){
        return Guid.NewGuid().ToString().ToUpper();
    }

}
