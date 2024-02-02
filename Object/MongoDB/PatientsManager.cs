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

    public async void UpdatePatientFirstName(string id, string firstName)
    {
        var filter = Builders<T>.Filter.Eq(p => p.GetId(), id);
        var update = Builders<T>.Update
            .Set(p => p.FirstName, firstName);
        await _mongoDB.collection.UpdateOneAsync(filter, update);
    }
    // TODO - Add more Update Method


}
