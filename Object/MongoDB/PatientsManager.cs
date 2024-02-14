using MongoDB.Bson;
using MongoDB.Driver;


namespace Object.MyDB;
public class PatientsManager<T> where T : Patient 
{
    // Manage API with MongoDB
    // TODO - Move to Config File
    private const string collectionName = "patients";
    private MongoDB<T> _mongoDB;

	public PatientsManager() 
    {
        this._mongoDB = new MongoDB<T>(collectionName);
	}

    public List<T> GetPatients()
    {
        return _mongoDB.GetItems();
    }

    public async void UpdatePatientFirstName(string id, string firstName)
    {
        var filter = Builders<T>.Filter.Eq(p => p.GetId(), id);
        var update = Builders<T>.Update
            .Set(p => p.FirstName, firstName);
        await _mongoDB.collection.UpdateOneAsync(filter, update);
    }
    public async void UpdatePatientLastName(string id, string lastName)
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
