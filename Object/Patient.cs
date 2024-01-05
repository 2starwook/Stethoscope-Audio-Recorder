

namespace Object.MyPatient;
public class Patient
{
    public readonly string id;
    public string Name {get; set;}
    // TODO - Add more information

	public Patient(string id, string name) {
        this.id = id;
        Name = name;
	}

}
