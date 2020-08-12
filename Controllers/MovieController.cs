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
    public class MovieController : Controller
    {
        IStoreMovies _movieStorage;

        public MovieController(IStoreMovies movieStorage)
        {
            _movieStorage = movieStorage;
        }

        /*** CREATE ***/
        public IActionResult Create() {
            ViewBag.Editing = false;
            return View("Upsert");
        }

        [HttpPost]
        public IActionResult Create(Movie myNewMovie)
        {
            _movieStorage.CreateMovie(myNewMovie);
            return RedirectToAction("Index");
        }


        /*** READ ***/
        public IActionResult Index()
        {
            var allMovies = _movieStorage.GetAllMovies();
            return View(allMovies);
        }

        public IActionResult Details(Guid id) {
           var movie = _movieStorage.GetMovieById(id);
           return View(movie);
        }

        /*** UPDATE ***/
        public IActionResult Edit(Guid id) {
            ViewBag.Editing = true;
            var movie = _movieStorage.GetMovieById(id);
            return View("Upsert", movie);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, Movie updatedMovie) {
            _movieStorage.UpdateMovie(id, updatedMovie);
            return RedirectToAction("Index");
        }


        /*** DELETE ***/
        [HttpPost]
        public IActionResult Delete(Guid id) {
            _movieStorage.DeleteMovie(id);
            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
