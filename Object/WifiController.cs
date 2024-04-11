using NET_MAUI_BLE.API;
using NET_MAUI_BLE.AppConfig;


namespace NET_MAUI_BLE.Object.Wifi;

public class WifiController 
{
    public WifiController()
    {
        _baseAddress = Config.HTTP_BASE_ADDRESS_TEST;
        _httpClientManager = new HttpClientManager(_baseAddress);
    }

    private HttpClientManager _httpClientManager;
    private string _baseAddress;

    public async Task<string> GetAudio()
    {
        var unique_id = FileAPI.GetUniqueID();
        var fileName = $"{unique_id}.wav";
        var binaryData = await _httpClientManager.GetBinaryAsync("/R10");
        FileAPI.WriteCacheData(fileName, binaryData);
        // TODO - Reusing a single path for temporary files
        return FileAPI.GetCachePath(fileName);
    }
}