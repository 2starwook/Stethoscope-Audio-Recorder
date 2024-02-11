using NET_MAUI_BLE.ViewModel;

namespace NET_MAUI_BLE.Pages;

public partial class AddRecordPage : ContentPage {

	public AddRecordPage(AddRecordViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
