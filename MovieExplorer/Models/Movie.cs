using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieExplorer.Models
{    public class Movie
    {
        // Link JSON "title" to this property
        [System.Text.Json.Serialization.JsonPropertyName("title")]
        public string Title { get; set; }

        // Link JSON "year" to this property
        [System.Text.Json.Serialization.JsonPropertyName("year")]
        public int Year { get; set; }

        // Store genres as a list of strings
        [System.Text.Json.Serialization.JsonPropertyName("genre")]
        public string[] Genres { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("director")]
        public string Director { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("rating")]
        public double ImdbRating { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("emoji")]
        public string Emoji { get; set; }

        // Join genre list into one string 
        public string GenresDisplay => Genres != null ? string.Join(", ", Genres) : "N/A";

        // Format rating with a star for better display
        public string RatingDisplay => $"⭐ {ImdbRating}";
    }
}
