using System;

namespace MoviesMVC.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string Director { get; set; }

        public int Year { get; set; }
    }
}
