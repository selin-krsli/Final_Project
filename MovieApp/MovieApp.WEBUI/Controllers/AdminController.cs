using Microsoft.AspNetCore.Mvc;
using MovieApp.BUSINESS.Abstract;
using MovieApp.ENTITY;
using MovieApp.WEBUI.Models;

namespace MovieApp.WEBUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMovieService _movieService;
        public AdminController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult MovieList()
        {
            var movieListViewModel = new MovieListViewModel
            {
                Movies = _movieService.GetAll()
            };
            return View(movieListViewModel);
        }
        public IActionResult CreateMovie()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMovie(MovieModel model)
        {
            var movieEntity = new Movie
            {
                MovieName = model.MovieName,
                Url = model.Url,
                MovieStory = model.MovieStory,
                Genre = model.Genre,
                Director = model.Director,
                Image = model.Image,
                Price = model.Price,
            };
            _movieService.Create(movieEntity);
            return RedirectToAction("MovieList");
        }
        public IActionResult EditMovie(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            else
            {
                var entity = _movieService.GetById((int)id);
                if(entity == null)
                {
                    return NotFound();
                }
                var model = new MovieModel()
                {
                    MovieId = entity.MovieId,
                    MovieName = entity.MovieName,
                    Url = entity.Url,
                    Price = entity.Price,
                    MovieStory = entity.MovieStory,
                    Genre = entity.Genre,
                    Director = entity.Director,
                    Image = entity.Image
                };
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult EditMovie(MovieModel model)
        {
            return View();
        }
    }
}
