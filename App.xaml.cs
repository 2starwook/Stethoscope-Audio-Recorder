using NET_MAUI_BLE.Object.DB;


namespace NET_MAUI_BLE;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//Craete a singleton DB manager
		DependencyService.Register<DBManager>();

		MainPage = new AppShell();
	}
}
