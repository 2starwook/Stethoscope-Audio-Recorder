using CommunityToolkit.Maui.Storage;


namespace Object.MyFileController;
public static class StorageAPI
{
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
}
