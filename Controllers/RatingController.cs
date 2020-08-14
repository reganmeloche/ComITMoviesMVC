using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesMVC.Models;
using MoviesMVC.DAL;

namespace MoviesMVC.Controllers
{
    public class RatingController : Controller
    {
        IStoreMovies _movieStorage;

        public RatingController(IStoreMovies movieStorage)
        {
            _movieStorage = movieStorage;
        }

        /*** CREATE ***/
        public IActionResult CreateForMovie(Guid movieId) {
            var rating = new Rating();
            rating.MovieId = movieId;
            return View(rating);
        }

        [HttpPost]
        public IActionResult Create(Rating newRating)
        {
            newRating.RatingDate = DateTime.Now;
            _movieStorage.AddRating(newRating);
            return RedirectToAction("Index", "Movie");
        }
    }
}
