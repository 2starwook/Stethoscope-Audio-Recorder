namespace Object.MyDB;
public class Patient : Item 
{
    public string FirstName {get; set;}
    public string LastName {get; set;}

	public Patient(string FirstName, string LastName) {
        this.FirstName = FirstName;
        this.LastName = LastName;
	}

}
