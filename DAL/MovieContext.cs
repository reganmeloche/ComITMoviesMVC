using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using MoviesMVC.Models;

namespace MoviesMVC.DAL
{
    public class MovieContext: DbContext {
        public MovieContext (DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }

    public class EFMovieStorage : IStoreMovies {

        private readonly MovieContext _movieContext;

        public EFMovieStorage(string connectionString) { 
            var optionsBuilder = new DbContextOptionsBuilder<MovieContext>();
            optionsBuilder.UseSqlServer(connectionString);
            _movieContext = new MovieContext(optionsBuilder.Options);
        }

        public List<Movie> GetAll() {
            var result = _movieContext.Movie.ToList();
            return result;
        }

        public Movie GetById(Guid movieId) {
            return _movieContext.Movie.FirstOrDefault(y => y.Id == movieId);
        }

        public Movie CreateMovie(Movie model) {
            var movieToCreate = new Movie() {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Director = model.Director,
                Year = model.Year
            };
            _movieContext.Add(movieToCreate);
            _movieContext.SaveChanges();
            return movieToCreate;
        }

        public void DeleteMovieById(Guid movieId) {
            var movie = _movieContext.Movie.FirstOrDefault(y => y.Id == movieId);
            _movieContext.Movie.Remove(movie);
            _movieContext.SaveChanges();
        }

        public void UpdateMovie(Guid movieId, Movie updatedMovie) {
            if (movieId != updatedMovie.Id)
            {
                throw new Exception("Invalid movie id");
            }

            _movieContext.Update(updatedMovie);
            _movieContext.SaveChanges();
        }

    }
}
