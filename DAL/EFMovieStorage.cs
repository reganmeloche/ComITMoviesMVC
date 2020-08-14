using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using MoviesMVC.Models;

namespace MoviesMVC.DAL {
    public class EFMovieStorage : IStoreMovies {
        
        MovieContext _context;

        public EFMovieStorage(MovieContext context) {
            _context = context;
        }

        public void CreateMovie(Movie myNewMovie) {
            myNewMovie.Id = Guid.NewGuid();
            _context.Add(myNewMovie);
            _context.SaveChanges();
        }

        public void UpdateMovie(Guid id, Movie updatedMovie) {
            if (id == updatedMovie.Id) {
                _context.Update(updatedMovie);
                _context.SaveChanges();
            } else {
                throw new Exception("Movie id does not match");
            }
            
        }

        public void DeleteMovie(Guid id) {
            var movie = GetMovieById(id);
            movie.IsDeleted = true;
            _context.Update(movie);
            _context.SaveChanges();
        }

        public Movie GetMovieById(Guid movieId) {
            var movie = _context.Movies
                .Include(x => x.Ratings)
                .FirstOrDefault(x => x.Id == movieId && x.IsDeleted == false);
            return movie;
        }

        public List<Movie> GetAllMovies() {
            var allMovies = _context.Movies
                .Include(x => x.Ratings)
                .Where(x => x.IsDeleted == false)
                .ToList();
            return allMovies;
        }

        public void AddRating(Rating newRating) {
            var movie = GetMovieById(newRating.MovieId);
            movie.Ratings.Add(newRating);
            _context.SaveChanges();
        }
    }

}