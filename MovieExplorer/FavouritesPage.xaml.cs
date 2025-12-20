namespace MovieExplorer;

public partial class FavouritesPage : ContentPage
{
    public FavouritesPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Load the specific history for the current user
        var history = await Services.HistoryService.LoadHistoryAsync();

        // Show the list of movies on the screen
        HistoryList.ItemsSource = history;
    }

    private async void OnClearAllClicked(object sender, EventArgs e)
    {
        // Ask the user if they are sure
        bool confirm = await DisplayAlert("Clear All", "Delete all your favourites?", "Yes", "No");

        if (confirm)
        {
            // Delete the JSON file from storage
            MovieExplorer.Services.HistoryService.ClearAllHistory();

            // Empty the list on the screen
            HistoryList.ItemsSource = null;
        }
    }
}