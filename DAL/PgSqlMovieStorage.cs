using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

using MoviesMVC.Models;

namespace MoviesMVC.DAL
{

    public class PgSqlMovieStorage : IStoreMovies {
        readonly NpgsqlConnection _conn;

        public PgSqlMovieStorage(NpgsqlConnection conn) {
            _conn = conn;
        }

        public Movie CreateMovie(Movie model) {
            var movieToCreate = new Movie() {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Director = model.Director,
                Year = model.Year
            };

            using (var cmd = new NpgsqlCommand(@"
                INSERT INTO movies (id, title, director, year, date_created) 
                VALUES (@a, @b, @c, @d, @e)",
                _conn))
            {
                cmd.Parameters.AddWithValue("a", movieToCreate.Id);
                cmd.Parameters.AddWithValue("b", movieToCreate.Title);
                cmd.Parameters.AddWithValue("c", movieToCreate.Director);
                cmd.Parameters.AddWithValue("d", movieToCreate.Year);
                cmd.Parameters.AddWithValue("e", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
            return movieToCreate;
        }

        public void UpdateMovie(Guid id, Movie model) {
            using (var cmd = new NpgsqlCommand(@"
                UPDATE movies 
                SET title = @b,
                    director = @c,
                    year = @d 
                WHERE id = @a",
                _conn))
            {
                cmd.Parameters.AddWithValue("a", id);
                cmd.Parameters.AddWithValue("b", model.Title);
                cmd.Parameters.AddWithValue("c", model.Director);
                cmd.Parameters.AddWithValue("d", model.Year);
                cmd.Parameters.AddWithValue("e", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMovieById(Guid id) {
            using (var cmd = new NpgsqlCommand(@"
                DELETE 
                FROM movies 
                WHERE id = @a",
                _conn))
            {
                cmd.Parameters.AddWithValue("a", id);
                cmd.ExecuteNonQuery();
            }
        }
    
        public List<Movie> GetAll() {
            var result = new List<Movie>();
            using (var cmd = new NpgsqlCommand("SELECT * FROM movies", _conn)) {
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        var nextResult = ConvertFromDb(reader);
                        result.Add(nextResult);
                    }
                    return result;
                } 
            }     
        }
    
        public Movie GetById(Guid movieId) {
            Movie result = null;
            using (var cmd = new NpgsqlCommand("SELECT * FROM movies where id = @a", _conn)) {          
                cmd.Parameters.AddWithValue("a", movieId);
                using (var reader = cmd.ExecuteReader()) {
                    var hasDbValue = reader.Read();
                    if (hasDbValue) {
                        result = ConvertFromDb(reader);
                    }
                    return result;
                } 
            }     
        }
    
        private Movie ConvertFromDb(NpgsqlDataReader reader) {
            Guid id = Guid.Parse(Convert.ToString(reader["id"]));
            string title = Convert.ToString(reader["title"]);
            string director = Convert.ToString(reader["director"]);
            int year = Convert.ToInt32(reader["year"]);

            return new Movie() {
                Id = id,
                Title = title,
                Director = director,
                Year = year
            };
        }
    }
}
