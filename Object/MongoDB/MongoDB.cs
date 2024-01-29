using MongoDB.Bson;
using MongoDB.Driver;


namespace Object.MyDB;
public class MongoDB<T> where T : Item {
    // Manage API with MongoDB
    // TODO - Move to Config File
    private const string dbName = "NET_MAUI_BLE";
    private const string password = "5QFun5SXJk5b5EMx";
    private const string mongoUri = $"mongodb+srv://2starwook:{password}@cluster0.jdq7pvv.mongodb.net/?retryWrites=true&w=majority";
    private IMongoClient client;
    public IMongoCollection<T> collection;
    
	public MongoDB(string collectionName) 
    {
        client = new MongoClient(mongoUri);
        collection = client.GetDatabase(dbName).GetCollection<T>(collectionName);
	}

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

    public void DeleteItems(string [] ids)
    {
        collection.DeleteMany(
            Builders<T>.Filter.In(
                p => p.Id, 
                Array.ConvertAll(ids, x => new ObjectId(x))
            )
        );
    }
}
