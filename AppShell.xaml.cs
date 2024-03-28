using NET_MAUI_BLE.Pages;


namespace NET_MAUI_BLE;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(RecordsPage), typeof(RecordsPage));
		Routing.RegisterRoute(nameof(RecordPage), typeof(RecordPage));
		Routing.RegisterRoute(nameof(AddRecordPage), typeof(AddRecordPage));
		Routing.RegisterRoute(nameof(AddPatientPage), typeof(AddPatientPage));
		BindingContext = this;
	}

	public void SetTabVisibility(bool visibility)
	{
		Shell.SetTabBarIsVisible(mainTab, visibility);
	}
}