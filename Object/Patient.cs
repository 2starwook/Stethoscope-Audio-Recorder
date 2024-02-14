namespace Object.MyDB;
public class Patient : Item 
{
	public Patient(string FirstName, string LastName) {
        this.FirstName = FirstName;
        this.LastName = LastName;
	}

    public string FirstName {get; set;}
    public string LastName {get; set;}


    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}
