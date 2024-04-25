using NAudio.Wave;
using NET_MAUI_BLE.AppConfig;


namespace NET_MAUI_BLE.API;

public static class FileAPI
{

    public static void WriteData(string filename, byte[] data)
    {
        var fullPath = Path.Combine(Config.rootPath, filename);
        File.WriteAllBytes(fullPath, data);
    }

    public static byte[] ReadData(string filename)
    {
        var fullPath = Path.Combine(Config.rootPath, filename);
        if (File.Exists(fullPath)){
            return File.ReadAllBytes(fullPath);
        }
        return null;
    }

    public static string GetFullPath(string filename)
    {
        return Path.Combine(Config.rootPath, filename);
    }

    public static void WriteCacheData(string filename, byte[] data)
    {
        var fullPath = Path.Combine(Config.rootCachePath, filename);
        File.WriteAllBytes(fullPath, data);
    }

    public static byte[] ReadCacheData(string filename)
    {
        var fullPath = Path.Combine(Config.rootCachePath, filename);
        if (File.Exists(fullPath)){
            return File.ReadAllBytes(fullPath);
        }
        return null;
    }

    public static string GetCachePath(string filename)
    {
        return Path.Combine(Config.rootCachePath, filename);
    }

    public static string GetAudioPath(string filename)
    {
        return Path.Combine(Config.audioDirPath, filename);
    }

    public static string GetImagePath(string filename)
    {
        return Path.Combine(Config.imageDirPath, filename);
    }

    public static void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }

    public static string [] GetFiles(string path)
    {
        return Directory.GetFiles(path);
    }

    public static string GetFileName(string path, bool withExtension=true)
    {
        if (withExtension)
        {
            return Path.GetFileName(path);
        }
        else
        {
            return Path.GetFileNameWithoutExtension(path);
        }
    }

    public static void DeleteFile(string path)
    {
        File.Delete(path);
    }

    /// <summary>
    /// Get "unique" hexadecimal upper id
    /// </summary>
    public static string GetUniqueID()
    {
        return Guid.NewGuid().ToString("n").ToUpper();
    }

    public static bool isExist(string path)
    {
        return Path.Exists(path);
    }

    public static void CreateDirIfNotExist(string path)
    {
        if (!FileAPI.isExist(path))
        {
            FileAPI.CreateDirectory(path);
        }
    }

    /// <summary>
    /// Check if given file is in WAV format
    /// </summary>
    public static bool isWavFormat(string path)
    {
        try
        {
            var reader = new WaveFileReader(path);
        }
        catch (FormatException)
        {
            return false;
        }
        return true;
    }
}