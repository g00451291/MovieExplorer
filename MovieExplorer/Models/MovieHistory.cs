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
        public DateTime Timestamp { get; set; }
        public bool IsFavourite { get; set; }
        
    }
}
