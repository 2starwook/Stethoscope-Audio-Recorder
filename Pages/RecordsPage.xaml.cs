using NET_MAUI_BLE.ViewModel;

namespace NET_MAUI_BLE.Pages;

public partial class RecordsPage : ContentPage
{
    public RecordsPage(RecordsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}