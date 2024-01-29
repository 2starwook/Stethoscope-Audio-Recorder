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

    public List<T> GetItems()
    {
        return _mongoDB.GetItems();
    }

    // TODO - Implement InsertItem(T item)
    // TODO - Implement DeleteItem(T item)

    public bool InsertItems(List<T> items)
    {
        return this._mongoDB.InsertItems(items);
    }

    public void DeleteItems(string [] ids)
    {
        this._mongoDB.DeleteItems(ids);
    }

    public async void UpdateRecordName(ObjectId id, string recordName)
    {
        var filter = Builders<T>.Filter.Eq(p => p.Id, id);
        var update = Builders<T>.Update
            .Set(p => p.recordName, recordName);
        await _mongoDB.collection.UpdateOneAsync(filter, update);
    }


}
