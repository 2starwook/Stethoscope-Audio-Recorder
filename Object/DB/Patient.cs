namespace NET_MAUI_BLE.Object.DB;

public class Patient : Item 
{
	public Patient(string firstName, string lastName) {
        this.FirstName = firstName;
        this.LastName = lastName;
	}
    // NOTE: Names should match with MongoDB
    public string FirstName {get; set;}
    public string LastName {get; set;}

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}
