using MyAPI;
using NET_MAUI_BLE.ViewModel;

namespace NET_MAUI_BLE.Pages;

public partial class AddPatientPage : ContentPage {

	public AddPatientPage(AddPatientViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MYAPI.UIAPI.HideTab();
    }
}
