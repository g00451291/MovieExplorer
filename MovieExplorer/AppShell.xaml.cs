namespace MovieExplorer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CheckUserStatus();
        }
        private async Task CheckUserStatus()
        {
            Preferences.Default.Clear(); // For testing purposes only. Remove this line in production.
            string userName = Preferences.Default.Get("UserName", string.Empty);

            if (string.IsNullOrEmpty(userName))
            {
                string result = await DisplayPromptAsync("Welcome", "Please enter your name to start:", "OK", cancel: null);

                if (!string.IsNullOrEmpty(result))
                {
                    Preferences.Default.Set("UserName", result.Trim());
                }
                else
                {
                    Preferences.Default.Set("UserName", "Guest");
                }
            }
        }
    }
}
