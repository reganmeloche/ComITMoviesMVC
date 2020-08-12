// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using MoviesMVC.Models;

// namespace MoviesMVC.Controllers
// {
//     public class RatingController : Controller
//     {
//         List<Movie> _movieList;

//         public RatingController(List<Movie> movieList)
//         {
//             _movieList = movieList;
//         }

//         /*** CREATE ***/
//         public IActionResult CreateForMovie(Guid movieId) {
//             var rating = new Rating();
//             rating.MovieId = movieId;
//             return View(rating);
//         }

//         [HttpPost]
//         public IActionResult Create(Rating newRating)
//         {
//             newRating.Id = Guid.NewGuid();
//             newRating.RatingDate = new DateTime();
//             var movie = GetMovieById(newRating.MovieId);
//             movie.Ratings.Add(newRating);
//             return RedirectToAction("Index", "Movie");
//         }

//         private Movie GetMovieById(Guid id) {
//             foreach (var movie in _movieList) { 
//                 if (id == movie.Id)
//                 {
//                     return movie;
//                 }
//             }
//             throw new Exception("Movie not found");
//         }



//     }
// }
