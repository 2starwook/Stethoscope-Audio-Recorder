using NET_MAUI_BLE.ViewModel;


namespace NET_MAUI_BLE.Pages;

public partial class WifiPage : ContentPage
{
	public WifiPage(WifiViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}