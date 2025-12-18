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

        var history = await Services.HistoryService.LoadHistoryAsync();

        HistoryList.ItemsSource = history;
    }

    private async void OnClearAllClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Clear All", "Delete all your favourites?", "Yes", "No");
        if (confirm)
        {
            MovieExplorer.Services.HistoryService.ClearAllHistory();
            HistoryList.ItemsSource = null; // Refresh the UI
        }
    }
}