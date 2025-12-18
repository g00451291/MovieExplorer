using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieExplorer.Models
{
    public class Movie
    {
        [System.Text.Json.Serialization.JsonPropertyName("title")]
        public string Title { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("year")]
        public int Year { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("genre")]
        public string[] Genres { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("director")]
        public string Director { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("rating")]
        public double ImdbRating { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("emoji")]
        public string Emoji { get; set; }

        // Helper properties for your UI labels
        public string GenresDisplay => Genres != null ? string.Join(", ", Genres) : "N/A";
        public string RatingDisplay => $"⭐ {ImdbRating}";
    }
}
