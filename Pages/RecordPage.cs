using NET_MAUI_BLE.ViewModel;

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
		(Shell.Current as AppShell).SetTabVisibility(false);
	}

	protected override void OnDisappearing (){
		base.OnDisappearing();
		(Shell.Current as AppShell).SetTabVisibility(true);
	}
}
