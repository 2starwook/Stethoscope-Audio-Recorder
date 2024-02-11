using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Object.MyData;
using NET_MAUI_BLE.Pages;

namespace NET_MAUI_BLE.ViewModel;

public partial class AddRecordViewModel : ObservableObject
{
	public AddRecordViewModel(DatabaseManager databaseManager)
	{
        this.databaseManager = databaseManager;
		patients = new ObservableCollection<string>();
        fileButtonText = "Select a File";
        // TODO - Once patients data is ready, add here
        //foreach (var each in databaseManager.GetPathList())
        //{
        //    Patients.Add(each);
        //}
    }

    private DatabaseManager databaseManager;

    [ObservableProperty]
    ObservableCollection<string> patients;

    [ObservableProperty]
	string recordName;

    [ObservableProperty]
	string filePath;

	[ObservableProperty]
	string nameLabel;

    [ObservableProperty]
    string fileButtonText;

	[RelayCommand]
	async Task Submit()
	{
		NameLabel = RecordName;
        await databaseManager.AddRecordDataAsync(FilePath, RecordName);
        await Shell.Current.GoToAsync($"{nameof(RecordsPage)}");
        // TODO - Add item to the current RecordsPage
        // TODO - Implement: Reset after clicking submit
	}

    [RelayCommand]
    async Task SelectFile()
    {
        var path = await databaseManager.ImportAudioFile();
        if (string.IsNullOrWhiteSpace(path))
            return;
        FilePath = path;
        var filename = Path.GetFileName(path);
        FileButtonText = filename;
    }

    [RelayCommand]
    async Task AddPatient()
    {
        await Shell.Current.GoToAsync($"{nameof(AddPatientPage)}");
    }
}
