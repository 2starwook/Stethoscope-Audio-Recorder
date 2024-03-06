using Object.MyData;


namespace NET_MAUI_BLE;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//Craete a singleton DB manager
		DependencyService.Register<DatabaseManager>();

		MainPage = new AppShell();
	}
}
