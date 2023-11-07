namespace BluetoothCourse.Extensions;

public static class GuidExtensions
{
    public static Guid UuidFromPartial(this Int32 @partial)
    {
        string id = @partial.ToString("X").PadRight(4, '0');

        if (id.Length == 4)
        {
            id = "0000" + id + "-0000-1000-8000-00805f9b34fb";
            //Default BLE uuid full 128 bits value
        }

        return Guid.ParseExact(id, "d");
    }
}