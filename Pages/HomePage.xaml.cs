using NET_MAUI_BLE.ViewModel;


namespace NET_MAUI_BLE.Pages;

public partial class HomePage : ContentPage {

	public HomePage(HomeViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
    }
}
