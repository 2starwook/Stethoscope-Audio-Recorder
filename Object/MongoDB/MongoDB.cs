using System.Diagnostics;
using MongoDB.Driver;
using MongoDB.Bson;
using NET_MAUI_BLE.AppConfig;


namespace NET_MAUI_BLE.Object.DB;

public class MongoDB<T> where T : Item {
    // Manage API with MongoDB
    // FIXME - MongoClient raises Exception on Other platform than MacOS
	public MongoDB(string collectionName) 
    {
        try
        {
            _client = new MongoClient(Config.MONGO_URI);
        }
        catch (Exception e)
        {
            Debug.WriteLine("There was a problem connecting to your " +
                    "Atlas cluster. Check that the URI includes a valid " +
                    "username and password, and that your IP address is " +
                    $"in the Access List. Message: {e.Message}");
            throw new Exception("Failed to connect with Mongo DB");
        }
        collection = _client.GetDatabase(Config.DB_NAME).GetCollection<T>(collectionName);
	}

    private IMongoClient _client;
    public IMongoCollection<T> collection;

    public async Task<List<T>> GetItemsAsync()
    {
        return (await collection.FindAsync(Builders<T>.Filter.Empty)).ToList();
    }

    public async Task InsertItemAsync(T item)
    {
        await collection.InsertOneAsync(item);
    }

    public async Task DeleteItemAsync(string id)
    {
        await collection.DeleteOneAsync(
            Builders<T>.Filter.Eq(p => p.Id, new ObjectId(id))
        );
    }
}
