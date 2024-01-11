using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

using Object.MyData;
using Object.MyAudio;


namespace NET_MAUI_BLE.ViewModel;

public partial class FileViewModel : ObservableObject
{
    public FileViewModel(IAudioManager audioManager)
    {
        audioController = new AudioController(audioManager);
        databaseManager = new DatabaseManager();
        items = new ObservableCollection<string>();
        foreach(var each in databaseManager.GetPathList()) {
            Items.Add(each);
        }
    }

    [ObservableProperty]
    ObservableCollection<string> items;

    private DatabaseManager databaseManager;

    private AudioController audioController;
    

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
	void Play(string path)
    {
		audioController.OpenFile(path);
		audioController.Play();
		audioController.AddEventHandler(new EventHandler(HandlePlayEnded));
	}

	void HandlePlayEnded(object sender, EventArgs e){
		// PlayBtn.Text = "Play";
	}
}