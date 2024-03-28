using CommunityToolkit.Mvvm.Messaging.Messages;
using NET_MAUI_BLE.Enum;


namespace NET_MAUI_BLE.Message.BleMessage;

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
