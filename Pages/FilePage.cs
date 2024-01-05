using Plugin.Maui.Audio;

using Object.MyAudio;
using Object.MyData;
using MYAPI;


namespace NET_MAUI_BLE.Pages;

public partial class FilePage : ContentPage {
	private DatabaseManager databaseManager;
	private AudioController audioController;

	// TODO - Implement: Page for each record (Play/Pause/Stop Sound / Rename Title / Share / Exam Details )
	// TODO - Implement: Rename each record (not real data name instead of path)
	// TODO - Idea: (From left to right - Setting / Home / Records)
	// Setting - Background connection time (Default 5 min) / Recording length (default 15sec)
	// Home - Record the sound (Save / Create new patient option)
	// Records - Show list of records by each patients (Search bar, if there is any record / No Record, Otherwise)
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
		frameStackLayout.Add(UIAPI.CreateButton("Play",
			(sender, e) => OnPlayButtonClicked(path), "button_"+path));
		frameStackLayout.Add(UIAPI.CreateButton("Delete",
			(sender, e) => OnDeleteButtonClicked(path)));
        frame.Content = frameStackLayout;
		return frame;
	}

	private void OnDeleteButtonClicked(string path){
		databaseManager.DeleteData(path);
		UpdateFileListUI();
	}

	private void OnPlayButtonClicked(string path){
		audioController.OpenFile(path);
		audioController.Play();
		audioController.AddEventHandler(new EventHandler(HandlePlayEnded));
	}

	void HandlePlayEnded(object sender, EventArgs e){
		// PlayBtn.Text = "Play";
	}

}
