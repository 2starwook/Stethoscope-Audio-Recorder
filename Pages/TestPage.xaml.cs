using NET_MAUI_BLE.ViewModel;

namespace NET_MAUI_BLE.Pages;

public partial class TestPage : ContentPage
{

    public TestPage(TestViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

