using MongoDB.Bson;
using Realms;
using NET_MAUI_BLE.Services;

namespace NET_MAUI_BLE.Models;

public partial class Item_ : IRealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [MapTo("owner_id")]
    [Required]
    public string OwnerId { get; set; }

    [MapTo("RecordName")]
    [Required]
    public string RecordName { get; set; }

    [MapTo("BinaryData")]
    [Required]
    public byte[] BinaryData { get; set; }

    public bool IsMine => OwnerId == RealmService.CurrentUser.Id;

    public string GetId()
    {
        return Id.ToString();
    }

    public void SetId(string id)
    {
        Id = new ObjectId(id);
    }
}