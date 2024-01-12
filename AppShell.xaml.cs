using NET_MAUI_BLE.Pages;

namespace NET_MAUI_BLE;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(RecordPage), typeof(RecordPage));
	}
}
