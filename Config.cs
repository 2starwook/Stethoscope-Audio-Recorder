namespace NET_MAUI_BLE.AppConfig;

public class Config
{
    public static string SERVICE_UUTD = "4fafc201-1fb5-459e-8fcc-c5c9c331914b";
    // public static string SERVICE_UUTD = "180d";

    public static string CHARACTERISTIC_UUID = "beb5483e-36e1-4688-b7f5-ea07361b26a8";
    // public static string CHARACTERISTIC_UUID = "2a37";

    public static string appDir = "NET_MAUI_BLE";

    public static string imageDir = "image";

    public static string audioDir = "audio";

    public static string dataDir = "data";

    public static string rootPath = FileSystem.Current.AppDataDirectory;

    public static string rootCachePath = FileSystem.Current.CacheDirectory;

    public static string appDirPath = Path.Combine(rootCachePath, appDir);

    public static string imageDirPath = Path.Combine(appDirPath, imageDir);

    public static string audioDirPath = Path.Combine(appDirPath, audioDir);

    public static string dataDirPath = Path.Combine(rootPath, dataDir);

    public static string HTTP_BASE_ADDRESS = "http://192.168.4.1:1337";
    // Base address for HTTP on Arduino chip

    public static string HTTP_BASE_ADDRESS_TEST = "http://127.0.0.1:8000";
    // Base address for HTTP on local testing server
}