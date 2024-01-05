using CommunityToolkit.Maui.Storage;


namespace MyAPI;
public static class StorageAPI
{
    public static async Task<string> ExportData(string filename, byte[] data){
        // Export Data with given name to the selected path on device
        using var stream = new MemoryStream(data);
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        #pragma warning disable CA1416 // Validate platform compatibility
        var path = await FileSaver.Default.SaveAsync(
            filename, stream, cancellationTokenSource.Token);
        #pragma warning restore CA1416 // Validate platform compatibility
        return path.FilePath;
    }

    public static async Task<string> GetFolderPath(){
        // Select Folder Path from the device
        CancellationToken cancellationToken = new CancellationToken();
		#pragma warning disable CA1416 // Validate platform compatibility
        var pickedFolder = await FolderPicker.Default.PickAsync(cancellationToken);
		#pragma warning restore CA1416 // Validate platform compatibility
        return pickedFolder.Folder.Path;
    }

    public static async Task<string> GetFilePath(){
        // Select File Path from the device
		#pragma warning disable CA1416 // Validate platform compatibility
        var pickedFile = await FilePicker.Default.PickAsync();
		#pragma warning restore CA1416 // Validate platform compatibility
        return pickedFile.FullPath;
    }
}
