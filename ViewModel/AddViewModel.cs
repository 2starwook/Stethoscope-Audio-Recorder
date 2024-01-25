using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

using Object.MyData;

namespace NET_MAUI_BLE.ViewModel;

public partial class AddViewModel : ObservableObject
{
	public AddViewModel(DatabaseManager databaseManager)
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
	string name;

	[ObservableProperty]
	string nameLabel;

    [ObservableProperty]
    string fileButtonText;

	[RelayCommand]
	void Submit()
	{
		NameLabel = Name;
	}

    [RelayCommand]
    async Task SelectFile()
    {
        var path = await databaseManager.ImportAudioFile();
        if (string.IsNullOrWhiteSpace(path))
            return;
        var filename = Path.GetFileName(path);
        FileButtonText = filename;
    }
}
