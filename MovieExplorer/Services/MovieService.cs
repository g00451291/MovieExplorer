using System.Text.Json;
using MovieExplorer.Models;

namespace MovieExplorer.Services
{
    public static class Movieservice
    {
        private const string Url = "https://raw.githubusercontent.com/DONH-ITS/jsonfiles/refs/heads/main/moviesemoji.json";

        private static readonly string CachePath = Path.Combine(FileSystem.AppDataDirectory, "movies_cache.json");

        public static async Task<List<Movie>> GetMoviesAsync()
        {
            string json;

            if(File.Exists(CachePath))
            {
                json = await File.ReadAllTextAsync(CachePath);
            }
            else
            {
                using var client = new HttpClient();
                json = await client.GetStringAsync(Url);
                await File.WriteAllTextAsync(CachePath, json);
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<Movie>>(json, options) ?? new List<Movie>();
        }
    }
}
