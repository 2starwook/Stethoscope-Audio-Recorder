using CommunityToolkit.Mvvm.Messaging.Messages;
using MyEnum;


namespace Object.MyMessage;
public class BleDataMessage : ValueChangedMessage<byte[]>
{
	public BleDataMessage(byte[] value) : base(value)
	{

	}
}


public class BleStatusMessage : ValueChangedMessage<BleStatus>
{
	public BleStatusMessage(BleStatus value) : base(value)
	{

	}
}
