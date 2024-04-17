using CommunityToolkit.Mvvm.Messaging.Messages;


namespace NET_MAUI_BLE.Message.DbMessage;

public class AddRecordMessage : ValueChangedMessage<string>
{
    public AddRecordMessage(string value) : base(value)
    {

    }
}
