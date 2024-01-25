using NET_MAUI_BLE.ViewModel;
using MyAPI;

namespace NET_MAUI_BLE.Pages;

public partial class RecordPage : ContentPage 
{
	public RecordPage(RecordViewModel vm) 
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override void OnAppearing (){
		base.OnAppearing();
		MYAPI.UIAPI.HideTab();
	}

	protected override void OnDisappearing (){
		base.OnDisappearing();
		MYAPI.UIAPI.ShowTab();
	}
}
