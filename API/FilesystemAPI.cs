using NET_MAUI_BLE.AppConfig;


namespace NET_MAUI_BLE.API;
static public class FilesystemAPI
{
    static public void Init()
    {
        FileAPI.CreateDirIfNotExist(Config.appDirPath);
        FileAPI.CreateDirIfNotExist(Config.imageDirPath);
        FileAPI.CreateDirIfNotExist(Config.audioDirPath);
    }

    static public string GetAudioFilePath(string id, string fileExtension="wav")
    {
        var fileName = $"{id}.{fileExtension}";
        return FileAPI.GetAudioPath(fileName);
    }

    static public bool IsAudioExist(string id, string fileExtension="wav")
    {
        return FileAPI.isExist(GetAudioFilePath(id, fileExtension));
    }

    static public string GetImageFilePath(string id, string fileExtension = "png")
    {
        var fileName = $"{id}.{fileExtension}";
        return FileAPI.GetImagePath(fileName);
    }

    static public bool IsImageExist(string id, string fileExtension = "png")
    {
        return FileAPI.isExist(GetImageFilePath(id, fileExtension));
    }

    static public void DeleteFile(string id)
    {
        if (IsImageExist(id))
        {
            FileAPI.DeleteFile(GetImageFilePath(id));
        }
        if (IsAudioExist(id))
        {
            FileAPI.DeleteFile(GetAudioFilePath(id));
        }
    }
}