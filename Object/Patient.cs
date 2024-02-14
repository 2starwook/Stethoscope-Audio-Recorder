namespace Object.MyDB;
public class Patient : Item 
{
	public Patient(string firstName, string lastName) {
        this.FirstName = firstName;
        this.LastName = lastName;
	}

    public string FirstName {get; set;}
    public string LastName {get; set;}

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}
