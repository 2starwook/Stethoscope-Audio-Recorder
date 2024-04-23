using NET_MAUI_BLE.ViewModel;


namespace NET_MAUI_BLE.Pages;

public partial class HomePage : ContentPage
{

	public HomePage(HomeViewModel vm)
	{
		vm.AudioReceivedEvent += async (isSuccessful) => { await FadeInOut(isSuccessful); };
		InitializeComponent();
		BindingContext = vm;
    }

    public async Task FadeInOut(bool isSuccessful)
	{
		if (isSuccessful == true)
		{
			ResultMessage.Text = "Received";
        }
		else
		{
			ResultMessage.Text = "Failed to receive";
        }
        await ResultMessage.FadeTo(1f, 1000);
        await Task.Delay(2000);
        await ResultMessage.FadeTo(0f, 1000);
    }

}