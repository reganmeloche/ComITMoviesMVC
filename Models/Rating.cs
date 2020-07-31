using System;

namespace MoviesMVC.Models
{
    public class Rating
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        
        public string Username { get; set; }

        public int Value { get; set; }

        public DateTime RatingDate { get; set; }
    }
}
