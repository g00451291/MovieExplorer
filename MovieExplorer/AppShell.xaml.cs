namespace MovieExplorer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        // Runs when the app is first opening
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CheckUserStatus();
        }

        // Prompt user for their name at startup
        private async Task CheckUserStatus()
        {
            Preferences.Default.Clear();

            string userName = Preferences.Default.Get("UserName", string.Empty);

            // If no name is found, show a popup box
            if (string.IsNullOrEmpty(userName))
            {
                // Ask the user to type their name
                string result = await DisplayPromptAsync("Welcome", "Please enter your name to start:", "OK", cancel: null);

                if (!string.IsNullOrEmpty(result))
                {
                    // Save the typed name
                    Preferences.Default.Set("UserName", result.Trim());
                }
                else
                {
                    // Default to 'Guest' if they leave it blank
                    Preferences.Default.Set("UserName", "Guest");
                }
            }
        }
    }
}
