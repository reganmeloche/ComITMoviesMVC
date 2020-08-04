using System;
using System.Collections.Generic;
using MoviesMVC.Models;

namespace MoviesMVC.DAL
{

    public class MsSqlMovieStorage : IStoreMovies {
        private List<Movie> _movieList;

        public MsSqlMovieStorage(List<Movie> movieList){
            _movieList = movieList;
        }

        public List<Movie> GetAll() {
            return _movieList;
        }

        public Movie GetById(Guid movieId) {
            foreach (var movie in _movieList) {
                if (movie.Id == movieId) {
                    return movie;
                }
            }
            return null;
        }

        public Movie CreateMovie(Movie movie) {
            movie.Id = Guid.NewGuid();
            _movieList.Add(movie);
            return movie;
        }

        public void DeleteMovieById(Guid movieId) {
            foreach (var movie in _movieList) {
                if (movie.Id == movieId) {
                    _movieList.Remove(movie);
                }
            }
        }

        public void UpdateMovie(Guid movieId, Movie updatedMovie) {
            foreach (var movie in _movieList) {
                if (movie.Id == movieId) {
                    movie.Title = updatedMovie.Title;
                    movie.Director = updatedMovie.Director;
                    movie.Year = updatedMovie.Year;
                }
            }
        }
    }
}
