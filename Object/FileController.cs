using System.IO;
using System.Diagnostics;
using System.Text;
using CommunityToolkit.Maui.Storage;

namespace Object.MyFileController;
public class FileController
{
	private readonly IFileSaver _fileSaver;
    private readonly IFolderPicker _folderPicker;
	private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    private CancellationToken cancellationToken = new CancellationToken();
    private string rootPath = FileSystem.Current.AppDataDirectory;
    private string rootCachePath = FileSystem.Current.CacheDirectory;

	public FileController(IFileSaver fileSaver, IFolderPicker folderPicker) {
		this._fileSaver = fileSaver;
        this._folderPicker = folderPicker;
	}

    public void WriteData(string filename, byte[] data){
        var fullPath = Path.Combine(rootPath, filename);
        File.WriteAllBytes(fullPath, data);
    }

    public byte[] ReadData(string filename){
        var fullPath = Path.Combine(rootPath, filename);
        if (File.Exists(fullPath)){
            return File.ReadAllBytes(fullPath);
        }
        return null;
    }

    public void WriteCacheData(string filename, byte[] data){
        var fullPath = Path.Combine(rootCachePath, filename);
        File.WriteAllBytes(fullPath, data);
    }

    public byte[] ReadCacheData(string filename){
        var fullPath = Path.Combine(rootCachePath, filename);
        if (File.Exists(fullPath)){
            return File.ReadAllBytes(fullPath);
        }
        return null;
    }

    public async Task<string> ExportData(string filename, byte[] data){
        using var stream = new MemoryStream(data);
        // Save file
        #pragma warning disable CA1416 // Validate platform compatibility
        var path = await _fileSaver.SaveAsync(filename, stream, cancellationTokenSource.Token);
        #pragma warning restore CA1416 // Validate platform compatibility
        return path.FilePath;
    }

    public async Task<string> GetDeviceFolderPath(){
		#pragma warning disable CA1416 // Validate platform compatibility
        var pickedFolder = await _folderPicker.PickAsync(cancellationToken);
		#pragma warning restore CA1416 // Validate platform compatibility
        return pickedFolder.Folder.Path;
    }

}
