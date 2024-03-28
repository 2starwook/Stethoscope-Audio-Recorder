namespace NET_MAUI_BLE.Object.View;

public class RecordInfo
{
    public string Name { get; set; }
    public string Id { get; set; }
    public RecordInfo(string name, string id)
    {
        this.Name = name;
        this.Id = id;
    }
}