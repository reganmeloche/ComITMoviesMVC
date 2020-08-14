using System;
using System.Collections.Generic;

using MoviesMVC.Models;

namespace MoviesMVC.DAL 
{
    public interface IStoreMovies {
        void CreateMovie(Movie myNewMovie);

        void UpdateMovie(Guid id, Movie updatedMovie);

        void DeleteMovie(Guid id);

        Movie GetMovieById(Guid id);

        List<Movie> GetAllMovies();

        void AddRating(Rating newRating);
    }

}