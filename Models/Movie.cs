using System;
using System.Collections.Generic;

namespace MoviesMVC.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Director { get; set; }

        public int Year { get; set; }

        public double AverageRating {
            get {
                if (Ratings == null || Ratings.Count == 0) { return 0 ;}
                double result = 0;
                foreach (var rating in Ratings) {
                    result += rating.Value;
                }
                return (double)result / Ratings.Count;
            }
        }

        public List<Rating> Ratings { get; set; }
        public bool IsDeleted {get;set;}
    }
}
