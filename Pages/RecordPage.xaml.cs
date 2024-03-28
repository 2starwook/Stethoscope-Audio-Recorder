using NET_MAUI_BLE.ViewModel;


namespace NET_MAUI_BLE.Pages;

public partial class RecordPage : ContentPage
{
    public RecordPage(RecordViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
