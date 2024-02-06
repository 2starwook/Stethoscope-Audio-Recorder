using Object.MyAudio;
using MyAPI;
using MyConfig;
using Object.MyDB;


namespace Object.MyData;
public class DatabaseManager
{
    // Manage Database: Add/Remove/Modify data
    private PatientsManager<Patient> patientsManager;
    private RecordsManager<Record> recordsManager;
    public Dictionary<string, Record> currentRecords;
    
	public DatabaseManager() {
        this.patientsManager = new PatientsManager<Patient>();
        this.recordsManager = new RecordsManager<Record>();
        currentRecords = new Dictionary<string, Record>();
        foreach (Record r in recordsManager.GetItems())
        {
            currentRecords.Add(r.GetId(), r);
        }
	}

    public async Task AddRecordDataAsync(string audioFilePath, string recordName){
        var record = new Record(recordName, File.ReadAllBytes(audioFilePath), null);
        await recordsManager.InsertItemAsync(record);
        currentRecords.Add(FileAPI.GetUniqueID(), record);
    }

    public async Task DeleteRecordDataAsync(string id){
        await recordsManager.DeleteItemAsync(id);
        currentRecords.Remove(id);
    }

    #pragma warning disable CS1998
    public async Task AddPatientDataAsync(){
        // TODO - Implement Later
    }
    #pragma warning restore CS1998

    #pragma warning disable CS1998
    public async Task DeletePatientDataAsync(string id){
        // TODO - Implement Later
    }
    #pragma warning restore CS1998


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
        var filename = FileAPI.GetUniqueID() + ".wav";
        var dstPath = Path.Combine(Config.dataDirPath, filename);
        File.Copy(srcPath.ToString(), dstPath);
        return dstPath;
    }

    // public List<string> GetPathList(){
    //     return recordCollection.GetPathList();
    // }

    // public List<Record> GetRecords(){
    //     return recordsManager.GetItems();
    // }


}
