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
    public Dictionary<string, Patient> currentPatients;
    
	public DatabaseManager() {
        this.patientsManager = new PatientsManager<Patient>();
        this.recordsManager = new RecordsManager<Record>();
        currentRecords = new Dictionary<string, Record>();
        foreach (Record r in recordsManager.GetRecords())
        {
            currentRecords.Add(r.GetId(), r);
        }
        currentPatients = new Dictionary<string, Patient>();
        foreach (Patient p in patientsManager.GetPatients())
        {
            currentPatients.Add(p.GetId(), p);
        }
	}

    public async Task AddRecordAsync(string audioFilePath, string recordName){
        var record = new Record(recordName, File.ReadAllBytes(audioFilePath), null);
        await recordsManager.InsertRecordAsync(record);
        currentRecords.Add(FileAPI.GetUniqueID(), record);
    }

    public async Task DeleteRecordAsync(string id){
        await recordsManager.DeleteRecordAsync(id);
        currentRecords.Remove(id);
    }

    public async Task AddPatientAsync(string firstName, string lastName){
        var patient = new Patient(firstName, lastName);
        await patientsManager.InsertPatientAsync(patient);
        currentPatients.Add(FileAPI.GetUniqueID(), patient);
    }

    public async Task DeletePatientAsync(string id){
        await patientsManager.DeletePatientAsync(id);
        currentPatients.Remove(id);
    }

    public void AttachPatientToRecord(string audioFilePath, string patientId){
        // TODO - Implement Later
    }

    public void DetachAssignedPatientFromRecord(string audioFilePath){
        // TODO - Implement Later
    }

    public async Task<string> ImportAudioFile(){
        var srcPath = await StorageAPI.GetFilePath();
        var filename = FileAPI.GetUniqueID() + ".wav";
        var dstPath = Path.Combine(Config.dataDirPath, filename);
        File.Copy(srcPath.ToString(), dstPath);
        return dstPath;
    }
}
