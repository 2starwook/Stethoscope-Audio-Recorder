namespace NET_MAUI_BLE.AppConfig;

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

    public static string DB_NAME = "NET_MAUI_BLE";
    // Name of database on MongoDB

    public static string USERNAME = "2starwook";
    // Username for MongoDB

    public static string PASSWORD = "xvaDWsxXWiTenwn0";
    // Password for MongoDB

    public static string MONGO_URI = $"mongodb+srv://{USERNAME}:{PASSWORD}@cluster0." +
        "jdq7pvv.mongodb.net/?retryWrites=true&w=majority";
    // URI for MongoDB

    public static string COLLECTION_PATIENTS = "patients";
    // Name of collection for patients

    public static string COLLECTION_RECORDS = "records";
    // Name of collection for records

    public static string HTTP_BASE_ADDRESS = "http://192.168.4.1:1337";
    // Base address for HTTP on Arduino chip

    public static string HTTP_BASE_ADDRESS_TEST = "http://127.0.0.1:8000";
    // Base address for HTTP on local testing server
}