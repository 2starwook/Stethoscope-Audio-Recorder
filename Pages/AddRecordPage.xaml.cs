using MyAPI;
using NET_MAUI_BLE.ViewModel;

namespace NET_MAUI_BLE.Pages;

public partial class AddRecordPage : ContentPage {
	// Controller settings for stethoscope device
	public AddRecordPage(AddRecordViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MYAPI.UIAPI.HideTab();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        MYAPI.UIAPI.ShowTab();
    }
}
