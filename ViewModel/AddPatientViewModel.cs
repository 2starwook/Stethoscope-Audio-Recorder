using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Object.MyData;
using NET_MAUI_BLE.Pages;

namespace NET_MAUI_BLE.ViewModel;

public partial class AddPatientViewModel : ObservableObject
{
	public AddPatientViewModel(DatabaseManager databaseManager)
	{
        this.databaseManager = databaseManager;
    }

    private DatabaseManager databaseManager;


    [ObservableProperty]
	string patientFirstName;

    [ObservableProperty]
	string patientLastName;

    [ObservableProperty]
    DateTime dateOfBirth = DateTime.Now;

    [RelayCommand]
    void Appearing()
    {
        try
        {
            MYAPI.UIAPI.HideTab();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }

    [RelayCommand]
	async Task Submit()
	{
        //await databaseManager.AddRecordDataAsync(FilePath, RecordName);
        //await Shell.Current.GoToAsync($"{nameof(RecordsPage)}");
        // TODO - Add item to the current RecordsPage
        // TODO - Implement: Reset after clicking submit
	}
}
