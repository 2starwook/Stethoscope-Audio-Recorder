using System.IO;
using System.Diagnostics;
using System.Text;
using CommunityToolkit.Maui.Storage;

namespace Object.MyFileController;
public class FileController
{
	private readonly IFileSaver _fileSaver;
    private readonly IFolderPicker _folderPicker;
	CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    CancellationToken cancellationToken = new CancellationToken();

	public FileController(IFileSaver fileSaver, IFolderPicker folderPicker) {
		this._fileSaver = fileSaver;
        this._folderPicker = folderPicker;
	}

    public async Task<string> SaveFile(string filename, string data){
        using var stream = new MemoryStream(Encoding.Default.GetBytes(data));
        // Save file
        #pragma warning disable CA1416 // Validate platform compatibility
        var path = await _fileSaver.SaveAsync(filename, stream, cancellationTokenSource.Token);
        #pragma warning restore CA1416 // Validate platform compatibility
        return path.FilePath;
    }

    public async Task<string> PickFolder(){
		#pragma warning disable CA1416 // Validate platform compatibility
        var pickedFolder = await _folderPicker.PickAsync(cancellationToken);
		#pragma warning restore CA1416 // Validate platform compatibility
        return pickedFolder.Folder.Name;
    }

}
