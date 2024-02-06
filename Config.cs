
namespace MyConfig;
public class Config
{
    public static string SERVICE_UUTD = "4fafc201-1fb5-459e-8fcc-c5c9c331914b";
    // public static string SERVICE_UUTD = "180d";

    public static string CHARACTERISTIC_UUID = "beb5483e-36e1-4688-b7f5-ea07361b26a8";
    // public static string CHARACTERISTIC_UUID = "2a37";

    public static string dataDir = "data";

    public static string rootPath = FileSystem.Current.AppDataDirectory;

    public static string rootCachePath = FileSystem.Current.CacheDirectory;

    public static string dataDirPath = Path.Combine(rootPath, dataDir);

}
