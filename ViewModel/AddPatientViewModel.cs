using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using NET_MAUI_BLE.Object.DB;
using NET_MAUI_BLE.Message.DbMessage;
using NET_MAUI_BLE.API;

namespace NET_MAUI_BLE.ViewModel;

public partial class AddPatientViewModel : ObservableObject
{
	public AddPatientViewModel()
	{
        this._databaseManager = DependencyService.Get<DatabaseManager>();
    }

    private DatabaseManager _databaseManager;
    [ObservableProperty]
	private string patientFirstName;
    [ObservableProperty]
	private string patientLastName;
    [ObservableProperty]
    private DateTime dateOfBirth = DateTime.Now;

    [RelayCommand]
    void Appearing()
    {
        try
        {
            UIAPI.HideTab();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }

    [RelayCommand]
	async Task Submit()
	{
        var patientId = await _databaseManager.AddPatientAsync(PatientFirstName, PatientLastName);
        WeakReferenceMessenger.Default.Send(new AddPatientMessage(patientId));
        await Shell.Current.GoToAsync("..");
    }
}
