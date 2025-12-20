namespace MovieExplorer;

public partial class SettingPage : ContentPage
{
    public SettingPage()
    {
        InitializeComponent();
    }

    // Switch between Light and Dark mode
    private void OnThemeToggled(object sender, ToggledEventArgs e)
    {
        Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
    }

    //Change the app font size using a slider
    private void OnFontSizeChanged(object sender, ValueChangedEventArgs e)
    {
        double newSize = e.NewValue;

        // Update the label on this page to show the change immediately
        FontSizeLabel.FontSize = newSize;

        // Save the new font size so the app remembers it next time
        Preferences.Default.Set("AppFontSize", newSize);
    }
}