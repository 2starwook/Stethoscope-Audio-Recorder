using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Object.MyData;
using NET_MAUI_BLE.Pages;


namespace NET_MAUI_BLE.ViewModel;

public partial class FileViewModel : ObservableObject
{
    public FileViewModel()
    {
        databaseManager = new DatabaseManager();
        items = new ObservableCollection<string>();
        foreach(var each in databaseManager.GetPathList()) 
        {
            Items.Add(each);
        }
    }

    [ObservableProperty]
    ObservableCollection<string> items;

    private DatabaseManager databaseManager;

    [RelayCommand]
    async Task Add()
    {
        var path = await databaseManager.ImportAudioFile();
        if(string.IsNullOrWhiteSpace(path))
            return;
        
        Items.Add(path);
        databaseManager.AddData(path);
    }

    [RelayCommand]
    void Delete(string path)
    {
        Items.Remove(path);
        databaseManager.DeleteData(path);
    }

    [RelayCommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(RecordPage)}?Text={s}");
    }
}