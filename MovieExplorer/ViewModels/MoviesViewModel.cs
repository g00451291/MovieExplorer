using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieExplorer.Models;
using MovieExplorer.Services;

namespace MovieExplorer.ViewModels
{
    public class MoviesViewModel : BindableObject
    {
        private List<Movie> _allMovies = new();
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
            LoadMovies();
        }

        private async void LoadMovies()
        {
            var movies = await Movieservice.GetMoviesAsync();
            _allMovies = movies;

            PerformSearch();
        }

        private void PerformSearch()
        {
            var results = _allMovies.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                results = results.Where(m => m.Title.ToLower().Contains(SearchText.ToLower()));
            }

            FilteredMovies.Clear();
            foreach (var movie in results)
            {
                FilteredMovies.Add(movie);
            }
        }
    }
}
