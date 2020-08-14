using System;
using System.Collections.Generic;

using MoviesMVC.Models;

namespace MoviesMVC.DAL {
    public class ListMovieStorage : IStoreMovies {
        List<Movie> _movieList;

        public ListMovieStorage(List<Movie> movieList) {
            _movieList = movieList;
        }

        public void CreateMovie(Movie myNewMovie) {
            myNewMovie.Id = Guid.NewGuid();
            myNewMovie.Ratings = new List<Rating>();
            _movieList.Add(myNewMovie);
        }

        public void UpdateMovie(Guid id, Movie updatedMovie) {
            var movie = GetMovieById(id);
            movie.Title = updatedMovie.Title;
            movie.Director = updatedMovie.Director;
            movie.Year = updatedMovie.Year;
        }

        public void DeleteMovie(Guid id) {
            var movie = GetMovieById(id);
            _movieList.Remove(movie);
        }

        public Movie GetMovieById(Guid id) {
            

            foreach (var movie in _movieList) { 
                if (id == movie.Id)
                {
                    return movie;
                }
            }
            throw new Exception("Movie not found");
        }

        public List<Movie> GetAllMovies() {
            return _movieList;
        }

        public void AddRating(Rating newRating) {
            // Do nothing
        }
    }

}