using MongoDB.Driver;
using NET_MAUI_BLE.AppConfig;


namespace NET_MAUI_BLE.Object.DB;

public class RecordsManager<T> where T : Record
{
    // Manage API with MongoDB
	public RecordsManager() 
    {
        this._mongoDB = new MongoDB<T>(Config.COLLECTION_RECORDS);
	}

    private MongoDB<T> _mongoDB;

    public async Task<List<T>> GetRecordsAsync()
    {
        return await _mongoDB.GetItemsAsync();
    }

    public async Task InsertRecordAsync(T item)
    {
        await _mongoDB.InsertItemAsync(item);
    }

    public async Task DeleteRecordAsync(string id)
    {
        await _mongoDB.DeleteItemAsync(id);
    }

    public async void UpdateRecordNameAsync(string id, string recordName)
    {
        var filter = Builders<T>.Filter.Eq(p => p.GetId(), id);
        var update = Builders<T>.Update
            .Set(p => p.RecordName, recordName);
        await _mongoDB.collection.UpdateOneAsync(filter, update);
    }

}
