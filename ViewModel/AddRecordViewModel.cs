using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using NET_MAUI_BLE.Object.DB;
using Object.MyData;
using NET_MAUI_BLE.Object.View;
using NET_MAUI_BLE.Message.DbMessage;
using NET_MAUI_BLE.Pages;
using NET_MAUI_BLE.API;


namespace NET_MAUI_BLE.ViewModel;

public partial class AddRecordViewModel : ObservableObject, IRecipient<AddPatientMessage>
{
	public AddRecordViewModel()
	{
        this._databaseManager = DependencyService.Get<DatabaseManager>();
        WeakReferenceMessenger.Default.Register<AddPatientMessage>(this);
        FileButtonText = "Select a File";
    }

    private DatabaseManager _databaseManager;
    [ObservableProperty]
    private ObservableCollection<PatientInfo> patients;
    [ObservableProperty]
	private string recordName;
    [ObservableProperty]
	private string filePath;
    [ObservableProperty]
    private string fileButtonText;
    [ObservableProperty]
    private PatientInfo selectedPatient;

    public void Refresh()
    {
        FileButtonText = "Select a File";
        RecordName = "";
        FilePath = "";
        SelectedPatient = null;
    }

    private async Task LoadDataAsync()
    {
        await _databaseManager.LoadPatientDataAsync();
        Patients = new ObservableCollection<PatientInfo>();
        try
        {
            foreach (var patient in _databaseManager.currentPatients.Values)
            {
                AddPatient(patient);
            }
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"{e.ToString()}");
        }
    }

    private void AddPatient(Patient patient)
    {
        Patients.Add(new PatientInfo(patient.GetFullName(), patient.GetId()));
    }

    [RelayCommand]
    async Task Appearing()
    {
        try
        {
            UIAPI.HideTab();
            Refresh();
            if (Patients == null)
            {
                await LoadDataAsync();
            }
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
            UIAPI.ShowTab();
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
        await Shell.Current.GoToAsync("..");
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

    public void Receive(AddPatientMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Patient addedPatient;
            var res = _databaseManager.currentPatients.TryGetValue(message.Value, out addedPatient);
            AddPatient(addedPatient);
        });
    }
}