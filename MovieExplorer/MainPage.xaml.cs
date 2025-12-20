using MovieExplorer.Models;
using System.Text.Json;

namespace MovieExplorer
{
    public partial class MainPage : ContentPage
    {
        // List to store all movies from the file
        private List<Movie> _allMovies = new List<Movie>();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Get the saved font size from settings
            double savedFontSize = Preferences.Default.Get("AppFontSize", 18.0);

            if (_allMovies == null || _allMovies.Count == 0)
            {
                await LoadMoviesAsync();
            }
            else
            {
                MoviesList.ItemsSource = null;
                MoviesList.ItemsSource = _allMovies;
            }
        }

        private async Task LoadMoviesAsync()
        {
            try
            {
                // Open and read the movies.json file
                using var stream = await FileSystem.OpenAppPackageFileAsync("movies.json");
                using var reader = new StreamReader(stream);
                var contents = await reader.ReadToEndAsync();

                var movies = JsonSerializer.Deserialize<List<Movie>>(contents);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _allMovies = movies;
                    MoviesList.ItemsSource = null;
                    MoviesList.ItemsSource = _allMovies;
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Could not find movies.json", "OK");
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue?.ToLower() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Show all movies if search is empty
                MoviesList.ItemsSource = _allMovies;
            }
            else
            {
                // Filter movies by title or genre
                var filtered = _allMovies.Where(m =>
                    m.Title.ToLower().Contains(searchText) ||
                    (m.GenresDisplay != null && m.GenresDisplay.ToLower().Contains(searchText))
                ).ToList();

                MoviesList.ItemsSource = filtered;
            }
        }

        private async void OnFavouriteClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var movie = (Movie)button.CommandParameter;

            // Check if movie is already saved
            bool exists = await Services.HistoryService.IsAlreadyFavorited(movie.Title);
            if (exists)
            {
                await DisplayAlert("Note", "Already in favourites!", "OK");
                return;
            }

            // Save to the user's history file
            await Services.HistoryService.SaveToHistoryAsync(movie, true);
            await DisplayAlert("Success", $"{movie.Title} added!", "OK");
        }
    }
}
