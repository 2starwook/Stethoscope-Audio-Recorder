using MongoDB.Bson;


namespace NET_MAUI_BLE.Object.DB;

public class Item
{
    public ObjectId Id { get; set; }

    public string GetId()
    {
        return Id.ToString();
    }

    public void SetId(string id)
    {
        Id = new ObjectId(id);
    }
}