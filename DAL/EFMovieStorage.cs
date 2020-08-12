using System;
using System.Collections.Generic;
using System.Linq;

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
            _context.Remove(movie);
            _context.SaveChanges();
        }

        public Movie GetMovieById(Guid movieId) {
            var movie = _context.Movies.FirstOrDefault(nextMovie => nextMovie.Id == movieId);
            return movie;
        }

        public List<Movie> GetAllMovies() {
            var allMovies = _context.Movies.ToList();
            return allMovies;
        }
    }

}