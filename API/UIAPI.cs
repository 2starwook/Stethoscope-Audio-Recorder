namespace MYAPI;
public static class UIAPI{
	public static Button CreateButton(
		string text, EventHandler e=null, string styleId=null) {
		Button button = new Button { Text = text };
		if (e != null){
			button.Clicked += e;
		}
		if (styleId != null){
			button.StyleId = styleId;
		}
		return button;
	}
	public static Label CreateLabel(string text, double fontSize=5) {
		Label label = new Label { Text = text, FontSize=fontSize };
		return label;
	}
}