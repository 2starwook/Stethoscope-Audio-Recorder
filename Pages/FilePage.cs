using System.IO;
using System.Diagnostics;
using CommunityToolkit.Maui.Storage;
using Object.MyFileController;

using static Config;


namespace NET_MAUI_BLE.Pages;

public partial class FilePage : ContentPage {
	private FileController fileController;

	public FilePage(IFileSaver fileSaver, IFolderPicker folderPicker) {
		InitializeComponent();
		this.fileController = new FileController(fileSaver, folderPicker);
	}

	private async void SaveBtnClicked(object sender, EventArgs e) {
		try {
			await this.fileController.PickFolder();
		}
		catch {
			// Exception thrown when user cancels
		}
    }

}

