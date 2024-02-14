using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

using Object.MyData;


namespace NET_MAUI_BLE.ViewModel;

public partial class TestViewModel : ObservableObject
{
	public TestViewModel(DatabaseManager databaseManager)
	{
        this._databaseManager = databaseManager;
		patients = new ObservableCollection<string>();
        // TODO - Once patients data is ready, add here
        //foreach (var each in databaseManager.GetPathList())
        //{
        //    Patients.Add(each);
        //}
    }

    private DatabaseManager _databaseManager;
    [ObservableProperty]
    private ObservableCollection<string> patients;
    [ObservableProperty]
	private string name;
	[ObservableProperty]
	private string nameLabel;

	[RelayCommand]
	void Submit()
	{
		NameLabel = Name;
	}
}
