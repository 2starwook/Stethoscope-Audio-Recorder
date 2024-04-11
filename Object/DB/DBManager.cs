using NET_MAUI_BLE.API;
using NET_MAUI_BLE.AppConfig;


namespace NET_MAUI_BLE.Object.DB;

public class DBManager
{
    // Manage Database: Add/Remove/Modify data
	public DBManager()
    {
        this.patientsManager = new PatientsManager<Patient>();
        this.recordsManager = new RecordsManager<Record>();
        this.currentRecords = new Dictionary<string, Record>();
        this.isRecordDataLoaded = false;
        this.isPatientDataLoaded = false;
    }

    private PatientsManager<Patient> patientsManager;
    private RecordsManager<Record> recordsManager;
    private bool isRecordDataLoaded;
    private bool isPatientDataLoaded;

    public Dictionary<string, Record> currentRecords;
    public Dictionary<string, Patient> currentPatients;

    public async Task LoadDataAsync()
    {
        await LoadRecordDataAsync();
        await LoadPatientDataAsync();
    }

    public async Task LoadRecordDataAsync()
    {
        if (!isRecordDataLoaded)
        {
            isRecordDataLoaded = true;
            this.currentRecords = new Dictionary<string, Record>();
            foreach (Record r in await recordsManager.GetRecordsAsync())
            {
                this.currentRecords.Add(r.GetId(), r);
            }
        }
    }

    public async Task LoadPatientDataAsync()
    {
        if (!isPatientDataLoaded)
        {
            isPatientDataLoaded = true;
            this.currentPatients = new Dictionary<string, Patient>();
            foreach (Patient p in await patientsManager.GetPatientsAsync())
            {
                this.currentPatients.Add(p.GetId(), p);
            }
        }
    }

    public async Task<string> AddRecordAsync(string audioFilePath,
                                             string recordName)
    {
        var record = new Record(recordName, File.ReadAllBytes(audioFilePath), null);
        await recordsManager.InsertRecordAsync(record);
        currentRecords.Add(record.GetId(), record);
        return record.GetId();
    }

    public async Task DeleteRecordAsync(string id)
    {
        await recordsManager.DeleteRecordAsync(id);
        currentRecords.Remove(id);
    }

    public async Task<string> AddPatientAsync(string firstName, string lastName)
    {
        var patient = new Patient(firstName, lastName);
        await patientsManager.InsertPatientAsync(patient);
        currentPatients.Add(patient.GetId(), patient);
        return patient.GetId();
    }

    public async Task DeletePatientAsync(string id)
    {
        await patientsManager.DeletePatientAsync(id);
        currentPatients.Remove(id);
    }

    public void AttachPatientToRecord(string audioFilePath, string patientId)
    {
        // TODO - Implement Later
    }

    public void DetachAssignedPatientFromRecord(string audioFilePath)
    {
        // TODO - Implement Later
    }

    // TODO - Move to other object
    static public async Task<string> ImportAudioFile()
    {
        var srcPath = await StorageAPI.GetFilePath();
        var filename = FileAPI.GetUniqueID() + ".wav";
        var dstPath = Path.Combine(Config.dataDirPath, filename);
        File.Copy(srcPath.ToString(), dstPath);
        return dstPath;
    }
}