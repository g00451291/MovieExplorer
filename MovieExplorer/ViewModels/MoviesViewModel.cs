using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieExplorer.Models;
using MovieExplorer.Services;

namespace MovieExplorer.ViewModels
{
    public class MoviesViewModel : BindableObject
    {
        // Stores the full list of movies
        private List<Movie> _allMovies = new();

        // The list that shows up on the screen
        public ObservableCollection<Movie> FilteredMovies { get; set; } = new();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    PerformSearch();
                }
            }
        }

        public MoviesViewModel()
        {
            // Get the movies when this page is created
            LoadMovies();
        }

        private async void LoadMovies()
        {
            var movies = await Movieservice.GetMoviesAsync();
            _allMovies = movies;

            // Show movies on the screen
            PerformSearch();
        }

        // Filters the list based on what the user types
        private void PerformSearch()
        {
            var results = _allMovies.AsEnumerable();

            // If user typed something, filter by title
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                results = results.Where(m => m.Title.ToLower().Contains(SearchText.ToLower()));
            }

            // Clear the old list and add the new search results
            FilteredMovies.Clear();
            foreach (var movie in results)
            {
                FilteredMovies.Add(movie);
            }
        }
    }
}