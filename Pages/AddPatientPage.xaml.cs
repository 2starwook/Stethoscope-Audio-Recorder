using NET_MAUI_BLE.ViewModel;


namespace NET_MAUI_BLE.Pages;

public partial class AddPatientPage : ContentPage {

	public AddPatientPage(AddPatientViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}