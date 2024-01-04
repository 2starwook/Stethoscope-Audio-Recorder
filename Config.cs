
namespace MyConfig;
public class Config
{
    public static string serviceUUID = "180d";

    public static string characteristicUUID = "2a37";

    public static string dataDir = "data";

    public static string rootPath = FileSystem.Current.AppDataDirectory;

    public static string rootCachePath = FileSystem.Current.CacheDirectory;

    public static string dataDirPath = Path.Combine(rootPath, dataDir);

}
