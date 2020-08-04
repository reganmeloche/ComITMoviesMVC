using System;
using System.Collections.Generic;

using MoviesMVC.Models;

namespace MoviesMVC.DAL
{
    public interface IStoreMovies {
        List<Movie> GetAll();
        Movie GetById(Guid movieId);
        Movie CreateMovie(Movie movie);
        void DeleteMovieById(Guid movieId);
        void UpdateMovie(Guid movieId, Movie updatedMovie);
    }
}
