using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NET_MAUI_BLE.API;
using NET_MAUI_BLE.Models;


namespace NET_MAUI_BLE.ViewModel;

public partial class RecordViewModel : ObservableObject, IQueryAttributable
{
    private Item item;

    [ObservableProperty]
	private string recordId;

    [ObservableProperty]
    private string audioSource;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        item = query["item"] as Item;
        RecordId = item.GetId();
        AudioSource = FilesystemAPI.GetAudioFilePath(RecordId);
        if (!FilesystemAPI.IsAudioExist(RecordId))
        {
            FileAPI.WriteData(AudioSource, item.BinaryData);
        }
    }

    [RelayCommand]
    void Appearing()
    {
        try
        {
            UIAPI.HideTab();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
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
            Debug.WriteLine(e);
        }
    }
}