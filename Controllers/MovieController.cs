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

        public IActionResult Index()
        {
            // foreach (var movie in _movieList) {
            //     Console.WriteLine(movie.Title);
            //     Console.WriteLine(movie.Director);
            //     Console.WriteLine(movie.Year);
            // }

            return View(_movieList);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie myNewMovie)
        {
            myNewMovie.Id = Guid.NewGuid();
            _movieList.Add(myNewMovie);
            return RedirectToAction("Index");
        }

       public IActionResult Details(Guid id) {
            foreach (var movie in _movieList) { 
                if (id == movie.Id)
                {
                    return View(movie);
                }
            }
            return RedirectToAction("Error");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
