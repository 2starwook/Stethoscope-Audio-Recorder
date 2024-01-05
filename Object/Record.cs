using Object.MyPatient;


namespace Object.MyAudio;
public class Record
{
    private string audioPath;
    private bool isAssigned;
    private Patient patient;

	public Record(string audioPath) {
        this.audioPath = audioPath;
        this.isAssigned = false;
	}

    public string GetPath() {
        return this.audioPath;
    }

    public bool IsPatientAssigned(){
        return this.isAssigned;
    }

    public Patient getPatient() {
        return this.patient;
    }

    public void AssignPatient(Patient patient){
        this.patient = patient;
        this.isAssigned = true;
    }
}
