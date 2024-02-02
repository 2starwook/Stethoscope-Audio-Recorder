using Object.MyAudio;
using MyAPI;
using MyConfig;
using Object.MyDB;


namespace Object.MyData;
public class DatabaseManager {
    // Manage Database: Add/Remove/Modify data
    private RecordCollection recordCollection;
    private PatientsManager<Patient> patientsManager;
    private RecordsManager<Record> recordsManager;
    
	public DatabaseManager() {
        if(!File.Exists(Config.dataDirPath)){
            FileAPI.CreateDirectory(Config.dataDirPath);
        }
		var files = FileAPI.GetFiles(Config.dataDirPath);
        this.recordCollection = new RecordCollection(files);

        this.patientsManager = new PatientsManager<Patient>();
        this.recordsManager = new RecordsManager<Record>();
	}

    // TODO - Implement: Add/Delete Patient data

    // TODO - Implement: How to handle details about data such as fileName (SQL?)

    public async Task AddRecordDataAsync(string audioFilePath, string recordName){
        var record = new Record(recordName, File.ReadAllBytes(audioFilePath), null);
        await recordsManager.InsertItemAsync(record);
        // recordCollection.AddRecord(audioFilePath);
    }

    public async Task DeleteRecordDataAsync(string id){
        await recordsManager.DeleteItemAsync(id);
        // recordCollection.DeleteRecord(audioFilePath);
        // FileAPI.DeleteFile(audioFilePath);
    }

    public void AttachPatientToRecord(string audioFilePath, string patientId){
        // TODO - Implement Later
    }

    public void DetachAssignedPatientFromRecord(string audioFilePath){
        // TODO - Implement Later
    }

    // public bool IsDataExist(string audioFilePath){
    //     return recordCollection.IsExist(audioFilePath);
    // }

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

    public List<Record> GetRecords(){
        return recordsManager.GetItems();
    }

    private static string GetUniqueID(){
        return Guid.NewGuid().ToString().ToUpper();
    }

}
