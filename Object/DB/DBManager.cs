using NET_MAUI_BLE.API;
using NET_MAUI_BLE.AppConfig;


namespace NET_MAUI_BLE.Object.DB;

public class DBManager
{
    // Manage Database: Add/Remove/Modify data
	public DBManager()
    {
        // TODO - Implement: Check Network Connection
        this.recordsManager = new RecordsManager<Record>();
        this.currentRecords = new Dictionary<string, Record>();
        this.isRecordDataLoaded = false;
    }

    private RecordsManager<Record> recordsManager;
    private bool isRecordDataLoaded;
    public Dictionary<string, Record> currentRecords;

    public async Task LoadDataAsync()
    {
        await LoadRecordDataAsync();
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

    public async Task<string> AddRecordAsync(string audioFilePath,
                                             string recordName)
    {
        var record = new Record(recordName, File.ReadAllBytes(audioFilePath));
        currentRecords.Add(record.GetId(), record);
        return record.GetId();
    }

    public async Task DeleteRecordAsync(string id)
    {
        await recordsManager.DeleteRecordAsync(id);
        currentRecords.Remove(id);
    }
}