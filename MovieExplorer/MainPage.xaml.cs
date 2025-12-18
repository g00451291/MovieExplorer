namespace MovieExplorer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnFavouriteClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var movie = (MovieExplorer.Models.Movie)button.CommandParameter;

            bool exists = await MovieExplorer.Services.HistoryService.IsAlreadyFavorited(movie.Title);

            if (exists)
            {
                await DisplayAlert("Already Saved", $"{movie.Title} is already in your favourites!", "OK");
                return;
            }

            await MovieExplorer.Services.HistoryService.SaveToHistoryAsync(movie, true);
            await DisplayAlert("Success", $"{movie.Title} added to Favourites!", "OK");
        }
    }
}
