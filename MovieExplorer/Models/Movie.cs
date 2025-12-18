using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieExplorer.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string[] Genre { get; set; }
        public string Director { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("IMDB rating")]
        public double ImdbRating { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("Emoji representing the primry genre")]
        public string Emoji { get; set; }

    }
}
