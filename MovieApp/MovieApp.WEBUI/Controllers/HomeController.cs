﻿using Microsoft.AspNetCore.Mvc;
using MovieApp.BUSINESS.Abstract;
using MovieApp.WEBUI.Models;

namespace MovieApp.WEBUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult Index()
        {
            var movieViewModel = new MovieListViewModel()
            {
                Movies = _movieService.GetHomePageMovies()
            };

            return View(movieViewModel);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
