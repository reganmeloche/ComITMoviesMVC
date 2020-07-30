using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesMVC.Models;

namespace MoviesMVC.Controllers
{
    public class MovieController : Controller
    {
        List<Movie> _movieList;

        public MovieController(List<Movie> movieList)
        {
            _movieList = movieList;
        }

        /*** CREATE ***/
        public IActionResult Create() {
            ViewBag.Editing = false;
            return View("Upsert");
        }

        [HttpPost]
        public IActionResult Create(Movie myNewMovie)
        {
            myNewMovie.Id = Guid.NewGuid();
            _movieList.Add(myNewMovie);
            return RedirectToAction("Index");
        }


        /*** READ ***/
        public IActionResult Index()
        {
            return View(_movieList);
        }

        public IActionResult Details(Guid id) {
           var movie = GetById(id);
           return View(movie);
        }

        /*** UPDATE ***/
        public IActionResult Edit(Guid id) {
            ViewBag.Editing = true;
            var movie = GetById(id);
            return View("Upsert", movie);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, Movie updatedMovie) {
            var movie = GetById(id);
            movie.Title = updatedMovie.Title;
            movie.Director = updatedMovie.Director;
            movie.Year = updatedMovie.Year;
            return RedirectToAction("Index");
        }


        /*** DELETE ***/
        [HttpPost]
        public IActionResult Delete(Guid id) {
            var movie = GetById(id);
            _movieList.Remove(movie);
            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Movie GetById(Guid id) {
            foreach (var movie in _movieList) { 
                if (id == movie.Id)
                {
                    return movie;
                }
            }
            throw new Exception("Movie not found");
        }
    }
}
