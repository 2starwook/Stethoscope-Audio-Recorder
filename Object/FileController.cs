using CommunityToolkit.Maui.Storage;
using MyConfig;


namespace Object.MyFileController;
public static class FileController
{

    public static void WriteData(string filename, byte[] data){
        var fullPath = Path.Combine(Config.rootPath, filename);
        File.WriteAllBytes(fullPath, data);
    }

    public static byte[] ReadData(string filename){
        var fullPath = Path.Combine(Config.rootPath, filename);
        if (File.Exists(fullPath)){
            return File.ReadAllBytes(fullPath);
        }
        return null;
    }

    public static void WriteCacheData(string filename, byte[] data){
        var fullPath = Path.Combine(Config.rootCachePath, filename);
        File.WriteAllBytes(fullPath, data);
    }

    public static byte[] ReadCacheData(string filename){
        var fullPath = Path.Combine(Config.rootCachePath, filename);
        if (File.Exists(fullPath)){
            return File.ReadAllBytes(fullPath);
        }
        return null;
    }

    public static async Task<string> ExportData(string filename, byte[] data){
        using var stream = new MemoryStream(data);
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        #pragma warning disable CA1416 // Validate platform compatibility
        var path = await FileSaver.Default.SaveAsync(
            filename, stream, cancellationTokenSource.Token);
        #pragma warning restore CA1416 // Validate platform compatibility
        return path.FilePath;
    }

    public static async Task<string> GetDeviceFolderPath(){
        CancellationToken cancellationToken = new CancellationToken();
		#pragma warning disable CA1416 // Validate platform compatibility
        var pickedFolder = await FolderPicker.Default.PickAsync(cancellationToken);
		#pragma warning restore CA1416 // Validate platform compatibility
        return pickedFolder.Folder.Path;
    }

    public static async Task<string> GetDeviceFilePath(){
		#pragma warning disable CA1416 // Validate platform compatibility
        var pickedFile = await FilePicker.Default.PickAsync();
		#pragma warning restore CA1416 // Validate platform compatibility
        return pickedFile.FullPath;
    }

    public static void CreateDirectory(string path){
        Directory.CreateDirectory(path);
    }

    public static string [] GetFiles(string path){
        return Directory.GetFiles(path);
    }

}
