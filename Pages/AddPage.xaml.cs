using MyAPI;
using NET_MAUI_BLE.ViewModel;

namespace NET_MAUI_BLE.Pages;

public partial class AddPage : ContentPage {
	// Controller settings for stethoscope device
	public AddPage(AddViewModel vm)
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
