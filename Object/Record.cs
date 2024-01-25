using Object.MyPatient;


namespace Object.MyAudio;
public class Record
{
    private readonly string audioFilePath;
    private string fileName; // TODO - Add fileName 
    private bool isAssigned;
    private Patient assignedPatient;

	public Record(string audioFilePath) {
        this.audioFilePath = audioFilePath;
        this.isAssigned = false;
	}

    public string GetAudioFile() {
        return this.audioFilePath;
    }

    public bool IsPatientAssigned(){
        return this.isAssigned;
    }

    public Patient getAssignedPatient() {
        return this.assignedPatient;
    }

    public void AssignPatient(Patient patient){
        this.assignedPatient = patient;
        this.isAssigned = true;
    }

    public void DeleteAssignedPatient(){
        this.assignedPatient = null;
        this.isAssigned = false;
    }
}
