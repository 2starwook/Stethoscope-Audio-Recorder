using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;
using MyConfig;


namespace Object.MyDB;
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

    public List<T> GetItems()
    {
        return collection.Find(Builders<T>.Filter.Empty).ToList();
    }

    public bool InsertItems(List<T> items)
    {
        var isSuccessful = true;
        try
        {
            collection.InsertMany(items);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Something went wrong trying to insert the new data." +
                    $" Message: {e.Message}");
            isSuccessful = false;
        }
        return isSuccessful;
    }

    public async Task InsertItemAsync(T item)
    {
        await collection.InsertOneAsync(item);
    }

    public void DeleteItems(string [] ids)
    {
        collection.DeleteMany(
            Builders<T>.Filter.In(
                p => p.GetId(), 
                ids
            )
        );
    }

    public async Task DeleteItemAsync(string id){
        await collection.DeleteOneAsync(
            Builders<T>.Filter.Eq(p => p.GetId(), id)
        );
    }
}
