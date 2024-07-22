using Microsoft.AspNetCore.Mvc;
using MovieApp.BUSINESS.Abstract;
using MovieApp.ENTITY;
using MovieApp.WEBUI.Models;
using Newtonsoft.Json;

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

            var serializeObj = new MessageBoxInfo
            {
                Message = $"{movieEntity.MovieName} is added!",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(serializeObj);
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
                var movieEntity = _movieService.GetById((int)id);
                if(movieEntity == null)
                {
                    return NotFound();
                }
                var model = new MovieModel()
                {
                    MovieId = movieEntity.MovieId,
                    MovieName = movieEntity.MovieName,
                    Url = movieEntity.Url,
                    Price = movieEntity.Price,
                    MovieStory = movieEntity.MovieStory,
                    Genre = movieEntity.Genre,
                    Director = movieEntity.Director,
                    Image = movieEntity.Image
                };
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult EditMovie(MovieModel model)
        {
            var movieEntity = _movieService.GetById(model.MovieId);
            if(movieEntity == null)
            {
                return NotFound();
            }
            movieEntity.MovieName = model.MovieName;
            movieEntity.Url = model.Url;
            movieEntity.Price = model.Price;
            movieEntity.MovieStory = model.MovieStory;
            movieEntity.Genre = model.Genre;
            movieEntity.Director = model.Director;
            movieEntity.Image = model.Image;

            _movieService.Update(movieEntity);

            var serializeObj  = new MessageBoxInfo
            {
                Message = $"{movieEntity.MovieName} is updated!",
                AlertType = "warning"
            };

            TempData["message"] = JsonConvert.SerializeObject(serializeObj);
            return RedirectToAction("MovieList");
        }
        [HttpPost]
        public IActionResult DeleteMovie(int movieId)
        {
            var movieEntity = _movieService.GetById(movieId);
            if(movieEntity != null)
            {
                _movieService.Delete(movieEntity);
            }
            var serializeObj = new MessageBoxInfo
            {
                Message = $"{movieEntity.MovieName} is deleted!",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(serializeObj);
            return RedirectToAction("MovieList");
        }
    }
}
