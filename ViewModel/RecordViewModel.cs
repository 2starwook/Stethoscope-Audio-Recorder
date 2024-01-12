using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

using Object.MyData;
using Object.MyAudio;
using NET_MAUI_BLE.Pages;


namespace NET_MAUI_BLE.ViewModel;

[QueryProperty("Text", "Text")]
public partial class RecordViewModel : ObservableObject
{
    public RecordViewModel(IAudioManager audioManager)
    {
        audioController = new AudioController(audioManager);
    }

    private AudioController audioController;
	[ObservableProperty]
	string text;

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