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
            // Use the saved name, or 'Guest' if the prompt failed
            string name = Preferences.Default.Get("UserName", "Guest");
            return Path.Combine(FileSystem.AppDataDirectory, $"{name}_history.json");
        }

        public static async Task SaveToHistoryAsync(Movie movie, bool isFavourite)
        {
            var history = await LoadHistoryAsync();

            var entry = new MovieHistory
            {
                MovieTitle = movie.Title,
                Year = movie.Year,
                Emoji = movie.Emoji,
                Timestamp = DateTime.Now,
                IsFavourite = isFavourite
            };

            history.Add(entry);

            string json = JsonSerializer.Serialize(history);
            await File.WriteAllTextAsync(GetFilePath(), json);
        }

        public static async Task<List<MovieHistory>> LoadHistoryAsync()
        {
            string path = GetFilePath();
            if (!File.Exists(path)) return new List<MovieHistory>();

            string json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<MovieHistory>>(json) ?? new List<MovieHistory>();
        }
        public static async Task<bool> IsAlreadyFavorited(string title)
        {
            var history = await LoadHistoryAsync();
            return history.Any(m => m.MovieTitle == title);
        }

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
