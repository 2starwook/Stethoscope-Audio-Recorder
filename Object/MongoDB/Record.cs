namespace NET_MAUI_BLE.Object.DB;
public class Record : Item
{
	public Record(string recordName, byte[] binaryData, string assignedPatientId) {
        this.RecordName = recordName;
        this.BinaryData = binaryData;
        this.AssignedPatientId = assignedPatientId;
	}
    // NOTE: Names should match with MongoDB
    public string RecordName {get; set;}
    public byte[] BinaryData {get; set;}
    public string AssignedPatientId {get; set;}

    #pragma warning disable CS0649
    private readonly string audioFilePath;
    #pragma warning restore CS0649

    public string GetAudioFile() {
        return this.audioFilePath;
    }

    public bool IsPatientAssigned(){
        return this.AssignedPatientId != null;
    }

    public void AssignPatient(string patientId){
        this.AssignedPatientId = patientId;
    }

    public void DeleteAssignedPatient(){
        this.AssignedPatientId = null;
    }
}
