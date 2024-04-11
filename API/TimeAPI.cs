using System.Text;


namespace NET_MAUI_BLE.API;

static public class TimeAPI
{
    static public string GetCurrentDateTime()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmssffff");
    }
}