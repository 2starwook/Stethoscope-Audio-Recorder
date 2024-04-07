using System.Text;
using System.Text.Json;


namespace NET_MAUI_BLE.Object.Wifi;

public class WifiController 
{
    public WifiController()
    {
        _baseAddress = "";
        _httpClientManager = new HttpClientManager(_baseAddress);
    }

    private HttpClientManager _httpClientManager;
    private string _baseAddress;

}