using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieExplorer.Models
{
    public class MovieHistory
    {
        public string MovieTitle { get; set; }

        public int Year { get; set; }

        public string Emoji { get; set; }

        // Records the exact date and time the user saved the movie
        public DateTime Timestamp { get; set; }

        // True if the movie is marked as a favourite
        public bool IsFavourite { get; set; }
    }
}
