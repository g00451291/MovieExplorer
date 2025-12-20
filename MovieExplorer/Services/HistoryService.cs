using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using MovieExplorer.Models;

namespace MovieExplorer.Services
{
    public static class HistoryService
    {
        private static string GetFilePath()
        {
            //Use the saved name to create a unique file for each user
            string name = Preferences.Default.Get("UserName", "Guest");
            return Path.Combine(FileSystem.AppDataDirectory, $"{name}_history.json");
        }

        // Saves a movie to the history file
        public static async Task SaveToHistoryAsync(Movie movie, bool isFavourite)
        {
            var history = await LoadHistoryAsync();

            // Create a new history entry with the current time
            var entry = new MovieHistory
            {
                MovieTitle = movie.Title,
                Year = movie.Year,
                Emoji = movie.Emoji,
                Timestamp = DateTime.Now,
                IsFavourite = isFavourite
            };

            history.Add(entry);

            // Convert list to JSON and save to the phone's storage
            string json = JsonSerializer.Serialize(history);
            await File.WriteAllTextAsync(GetFilePath(), json);
        }

        // Reads the saved history file
        public static async Task<List<MovieHistory>> LoadHistoryAsync()
        {
            string path = GetFilePath();

            if (!File.Exists(path)) return new List<MovieHistory>();

            string json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<MovieHistory>>(json) ?? new List<MovieHistory>();
        }

        // Checks if a movie is already in the favourites list
        public static async Task<bool> IsAlreadyFavorited(string title)
        {
            var history = await LoadHistoryAsync();
            return history.Any(m => m.MovieTitle == title);
        }

        // Deletes the history file for the current user
        public static void ClearAllHistory()
        {
            string path = GetFilePath();
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
