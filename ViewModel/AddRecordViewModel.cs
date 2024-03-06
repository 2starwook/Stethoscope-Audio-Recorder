using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Object.MyData;
using Object.Frontend;
using Object.MyMessage;
using NET_MAUI_BLE.Pages;


namespace NET_MAUI_BLE.ViewModel;
public partial class AddRecordViewModel : ObservableObject
{
	public AddRecordViewModel()
	{
        this._databaseManager = DependencyService.Get<DatabaseManager>();
		patients = new ObservableCollection<PatientInfo>();
        foreach (var each in _databaseManager.currentPatients.Values)
        {
            patients.Add(new PatientInfo(each.GetFullName(), each.GetId()));
        }
    }

    private DatabaseManager _databaseManager;
    [ObservableProperty]
    private ObservableCollection<PatientInfo> patients;
    [ObservableProperty]
	private string recordName;
    [ObservableProperty]
	private string filePath;
    [ObservableProperty]
    private string fileButtonText = "Select a File";
    [ObservableProperty]
    private PatientInfo selectedPatient;

    void Refresh()
    {
        FileButtonText = "Select a File";
        RecordName = "";
        FilePath = "";
        SelectedPatient = null;
    }

    [RelayCommand]
    void Appearing()
    {
        try
        {
            MYAPI.UIAPI.HideTab();
            Refresh();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }

    [RelayCommand]
    void Disappearing()
    {
        try
        {
            MYAPI.UIAPI.ShowTab();
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }

    [RelayCommand]
	async Task Submit()
	{
        var recordId = await _databaseManager.AddRecordAsync(FilePath, RecordName);
        WeakReferenceMessenger.Default.Send(new AddRecordMessage(recordId));
        await Shell.Current.GoToAsync($"..");
    }

    [RelayCommand]
    async Task SelectFile()
    {
        var path = await _databaseManager.ImportAudioFile();
        if (string.IsNullOrWhiteSpace(path))
            return;
        FilePath = FileButtonText = path;
    }

    [RelayCommand]
    async Task AddPatient()
    {
        await Shell.Current.GoToAsync($"{nameof(AddPatientPage)}");
    }
}