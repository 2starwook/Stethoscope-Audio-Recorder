using MongoDB.Bson;
using MongoDB.Driver;


namespace Object.MyDB;
public class RecordsManager<T> where T : Record
{
    // Manage API with MongoDB
    // TODO - Move to Config File
    private const string collectionName = "records";
    private MongoDB<T> _mongoDB;

	public RecordsManager() 
    {
        this._mongoDB = new MongoDB<T>(collectionName);
	}

    public List<T> GetRecords()
    {
        return _mongoDB.GetItems();
    }

    // public bool InsertRecords(List<T> items)
    // {
    //     return this._mongoDB.InsertItems(items);
    // }

    // public void DeleteRecords(string [] ids)
    // {
    //     this._mongoDB.DeleteItems(ids);
    // }

    public async Task InsertRecordAsync(T item)
    {
        await _mongoDB.InsertItemAsync(item);
    }

    public async Task DeleteRecordAsync(string id)
    {
        await _mongoDB.DeleteItemAsync(id);
    }

    public async void UpdateRecordName(string id, string recordName)
    {
        var filter = Builders<T>.Filter.Eq(p => p.GetId(), id);
        var update = Builders<T>.Update
            .Set(p => p.recordName, recordName);
        await _mongoDB.collection.UpdateOneAsync(filter, update);
    }


}
