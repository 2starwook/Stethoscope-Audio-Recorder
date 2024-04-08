namespace NET_MAUI_BLE.Controls;

public partial class CustomControl : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomControl), propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (CustomControl)bindable;

        control.Titlelabel.Text = newValue as string;
    });

    public CustomControl()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => GetValue(TitleProperty) as string;
        set => SetValue(TitleProperty, value);
    }
}