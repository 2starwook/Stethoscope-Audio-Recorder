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

    // TODO - Implement: Add/Delete Patient data

    // TODO - Implement: How to handle details about data such as fileName (SQL?)

    // TODO - Try MongoDB for storing patient info
    // https://github.com/mongodb-university/atlas_starter_dotnet
    // https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-8.0

    public void AddData(string audioFilePath){
        recordCollection.AddRecord(audioFilePath);
    }

    public void DeleteData(string audioFilePath){
        recordCollection.DeleteRecord(audioFilePath);
        FileAPI.DeleteFile(audioFilePath);
    }

    public void AttachPatientToRecord(string audioFilePath, string patientId){
        // TODO - Implement Later
    }

    public void DetachAssignedPatientFromRecord(string audioFilePath){
        // TODO - Implement Later
    }

    public bool IsDataExist(string audioFilePath){
        return recordCollection.IsExist(audioFilePath);
    }

    public async Task<string> ImportAudioFile(){
        var srcPath = await StorageAPI.GetFilePath();
        var filename = GetUniqueID() + ".wav";
        var dstPath = Path.Combine(Config.dataDirPath, filename);
        File.Copy(srcPath.ToString(), dstPath);
        return dstPath;
    }

    public List<string> GetPathList(){
        return recordCollection.GetPathList();
    }

    private static string GetUniqueID(){
        return Guid.NewGuid().ToString().ToUpper();
    }

}
