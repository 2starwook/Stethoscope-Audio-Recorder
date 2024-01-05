using Object.MyDatabase;


namespace NET_MAUI_BLE.Pages;

public partial class FilePage : ContentPage {
	private DatabaseManager databaseManager;

	public FilePage()
	{
		this.databaseManager = new DatabaseManager();
        Title = "Combined StackLayouts demo";
        StackLayout stackLayout = new StackLayout { Margin = new Thickness(20) };
        stackLayout.Add(new Label { Text = "Primary colors" });
		List<string> pathList = databaseManager.GetPathList();
		foreach (string path in pathList){
			var frame = CreateFrame(path);
			stackLayout.Add(frame);
		}
        Content = stackLayout;
	}

	private Frame CreateFrame(string text) {
        Frame frame = new Frame {
			BorderColor = Colors.Black, Padding = new Thickness(5)};
		StackLayout frameStackLayout = new StackLayout {
			Orientation = StackOrientation.Horizontal, Spacing = 15};
		frameStackLayout.Add(CreateButton(text));
        frame.Content = frameStackLayout;
		return frame;
	}

	private Button CreateButton(string text, EventHandler e = null) {
		Button button = new Button { Text = text };
		if (e != null){
			button.Clicked += e;
		}
		return button;
	}
}
