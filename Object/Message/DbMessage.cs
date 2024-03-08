using CommunityToolkit.Mvvm.Messaging.Messages;


namespace Object.MyMessage;
public class AddRecordMessage : ValueChangedMessage<string>
{
    public AddRecordMessage(string value) : base(value)
    {

    }
}

public class AddPatientMessage : ValueChangedMessage<string>
{
    public AddPatientMessage(string value) : base(value)
    {

    }
}