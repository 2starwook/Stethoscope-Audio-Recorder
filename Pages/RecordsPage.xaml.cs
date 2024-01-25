using NET_MAUI_BLE.ViewModel;

namespace NET_MAUI_BLE.Pages;

public partial class RecordsPage : ContentPage
{

    // TODO - Implement: Page for each record (Play/Pause/Stop Sound / Rename Title / Share / Exam Details )
    // TODO - Implement: Rename each record (not real data name instead of path)
    // TODO - Idea: (From left to right - Setting / Home / Records)
    // Setting - Background connection time (Default 5 min) / Recording length (default 15sec)
    // Home - Record the sound (Save / Create new patient option)
    // Records - Show list of records by each patients (Search bar, if there is any record / No Record, Otherwise)
    public RecordsPage(RecordsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
