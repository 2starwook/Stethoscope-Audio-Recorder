using CommunityToolkit.Mvvm.Messaging.Messages;
using MyEnum;


namespace Object.MyMessage;
public class AddRecordMessage : ValueChangedMessage<string>
{
    public AddRecordMessage(string value) : base(value)
    {

    }
}