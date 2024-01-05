using Plugin.Maui.Audio;

using Object.MyAudio;
using Object.MyData;
using MYAPI;


namespace NET_MAUI_BLE.Pages;

public partial class FilePage : ContentPage {
	private DatabaseManager databaseManager;
	private AudioController audioController;

	// TODO - Connect AudioController/Player to DatabaseManager
	// TODO - Add EventHandler to each button to start music
	// TODO - Implement: Rename each data (not real data name)
	public FilePage(IAudioManager audioManager) {
		this.databaseManager = new DatabaseManager();
		this.audioController = new AudioController(audioManager);
        UpdateFileListUI();
	}

	protected override void OnAppearing(){
		base.OnAppearing();
		UpdateFileListUI();
	}

	private void UpdateFileListUI(){
        StackLayout stackLayout = new StackLayout { Margin = new Thickness(20) };
		List<string> pathList = databaseManager.GetPathList();
		foreach (string path in pathList){
			var frame = CreateFrame(path);
			stackLayout.Add(frame);
		}
        Content = stackLayout;
	}

	private Frame CreateFrame(string path) {
        Frame frame = new Frame {
			BorderColor = Colors.Black, Padding = new Thickness(5)};
		StackLayout frameStackLayout = new StackLayout {
			Orientation = StackOrientation.Horizontal, Spacing = 15};
		frameStackLayout.Add(UIAPI.CreateLabel(path));
		frameStackLayout.Add(UIAPI.CreateButton("delete",
			(sender, e) => OnDeleteButtonClicked(path)));
        frame.Content = frameStackLayout;
		return frame;
	}

	private void OnDeleteButtonClicked(string path){
		databaseManager.DeleteData(path);
		UpdateFileListUI();
	}

}
