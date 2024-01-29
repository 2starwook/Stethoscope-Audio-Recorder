using MongoDB.Bson;


namespace Object.MyDB;
public class Item
{
    public ObjectId Id { get; set; }

    public string GetId(){
        return this.Id.ToString();
    }

}
