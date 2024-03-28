using System.Text;


namespace NET_MAUI_BLE.API;

static public class DataConvertAPI
{
    static public byte[] ConvertString2Byte(string data)
    {
        return Encoding.Default.GetBytes(data);
    }

    static public byte[] Combine(byte[] first, byte[] second)
    {
        byte[] ret = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, ret, 0, first.Length);
        Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
        return ret;
    }
}