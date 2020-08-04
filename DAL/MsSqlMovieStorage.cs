using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using MoviesMVC.Models;

namespace MoviesMVC.DAL
{

    public class MsSqlMovieStorage : IStoreMovies {
        readonly string _connString;

        public MsSqlMovieStorage(string connString) {
            _connString = connString;
        }

        public Movie CreateMovie(Movie model) {
            var movieToCreate = new Movie() {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Director = model.Director,
                Year = model.Year
            };

            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var queryString = @"
                    INSERT INTO movies (id, title, director, year, date_created) 
                    VALUES (@a, @b, @c, @d, @e)
                ";
                using (var cmd = new SqlCommand(queryString, conn)) {
                    cmd.Parameters.AddWithValue("a", movieToCreate.Id);
                    cmd.Parameters.AddWithValue("b", movieToCreate.Title);
                    cmd.Parameters.AddWithValue("c", movieToCreate.Director);
                    cmd.Parameters.AddWithValue("d", movieToCreate.Year);
                    cmd.Parameters.AddWithValue("e", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
    
            return movieToCreate;
        }

        public void UpdateMovie(Guid id, Movie model) {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var queryString = @"
                    UPDATE movies 
                    SET title = @b,
                        director = @c,
                        year = @d 
                    WHERE id = @a";
                using (var cmd = new SqlCommand(queryString, conn)) {
                    cmd.Parameters.AddWithValue("a", id);
                    cmd.Parameters.AddWithValue("b", model.Title);
                    cmd.Parameters.AddWithValue("c", model.Director);
                    cmd.Parameters.AddWithValue("d", model.Year);
                    cmd.Parameters.AddWithValue("e", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void DeleteMovieById(Guid id) {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var queryString = @"
                    DELETE
                    FROM movies 
                    WHERE id = @a";
                using (var cmd = new SqlCommand(queryString, conn)) {
                    cmd.Parameters.AddWithValue("a", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    
        public List<Movie> GetAll() {
            var result = new List<Movie>();
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var queryString = @"SELECT * FROM movies";
                using (var cmd = new SqlCommand(queryString, conn)) {
                    using (var reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            var nextResult = ConvertFromDb(reader);
                            result.Add(nextResult);
                        }
                        return result;
                    } 
                }
            } 
        }

        public Movie GetById(Guid id) {
            Movie result = null;
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var queryString = @"SELECT * FROM movies WHERE id = @a";
                using (var cmd = new SqlCommand(queryString, conn)) {
                    cmd.Parameters.AddWithValue("a", id);
                    using (var reader = cmd.ExecuteReader()) {
                        var hasDbValue = reader.Read();
                        if (hasDbValue) {
                            result = ConvertFromDb(reader);
                        }
                        return result;
                    } 
                }
            } 
        }
    
        private Movie ConvertFromDb(SqlDataReader reader) {
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
