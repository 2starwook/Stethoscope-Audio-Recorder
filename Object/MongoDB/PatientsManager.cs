using MongoDB.Bson;
using MongoDB.Driver;
using MyConfig;


namespace NET_MAUI_BLE.Object.DB;
public class PatientsManager<T> where T : Patient 
{
    // Manage API with MongoDB
	public PatientsManager() 
    {
        this._mongoDB = new MongoDB<T>(Config.COLLECTION_PATIENTS);
	}

    private MongoDB<T> _mongoDB;

    public async Task<List<T>> GetPatientsAsync()
    {
        return await _mongoDB.GetItemsAsync();
    }

    public async void UpdatePatientFirstNameAsync(string id, string firstName)
    {
        var filter = Builders<T>.Filter.Eq(p => p.GetId(), id);
        var update = Builders<T>.Update
            .Set(p => p.FirstName, firstName);
        await _mongoDB.collection.UpdateOneAsync(filter, update);
    }
    public async void UpdatePatientLastNameAsync(string id, string lastName)
    {
        var filter = Builders<T>.Filter.Eq(p => p.GetId(), id);
        var update = Builders<T>.Update
            .Set(p => p.LastName, lastName);
        await _mongoDB.collection.UpdateOneAsync(filter, update);
    }

    public async Task InsertPatientAsync(T item)
    {
        await _mongoDB.InsertItemAsync(item);
    }

    public async Task DeletePatientAsync(string id)
    {
        await _mongoDB.DeleteItemAsync(id);
    }
}
