namespace MovieExplorer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            CheckUserStatus();
        }

        private async void CheckUserStatus()
        {
            string userName = Preferences.Default.Get("UserName", string.Empty);

            if (string.IsNullOrEmpty(userName))
            {
                string result = await DisplayPromptAsync("Welcome", "Please enter your name to start:", "OK", cancel: null);

                if (!string.IsNullOrEmpty(result))
                {
                    Preferences.Default.Set("UserName", result.Trim());
                    await DisplayAlert("Hello!", $"Welcome to Movie Explorer, {result}!", "Thanks");
                }
                else
                {
                    await DisplayAlert("Notice", "You did not enter a name. You can set it later in settings.", "OK");
                }
            }
        }
    }
}
