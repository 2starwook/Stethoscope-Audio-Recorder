namespace NET_MAUI_BLE.Object.DB;

public class Record : Item
{
	public Record(string recordName, byte[] binaryData)
    {
        this.RecordName = recordName;
        this.BinaryData = binaryData;
	}
    // NOTE: Names should match with MongoDB
    public string RecordName {get; set;}
    public byte[] BinaryData {get; set;}

    #pragma warning disable CS0649
    private readonly string audioFilePath;
    #pragma warning restore CS0649

    public string GetAudioFile()
    {
        return this.audioFilePath;
    }
}