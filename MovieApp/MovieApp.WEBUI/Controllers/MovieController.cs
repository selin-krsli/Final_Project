using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.BUSINESS.Abstract;
using MovieApp.ENTITY;
using MovieApp.WEBUI.ViewModels;

namespace MovieApp.WEBUI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult List()
        {
            var movieListViewModel = new MovieListViewModel
            {
                Movies = _movieService.GetAll()
            };
            return View(movieListViewModel);
        }
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var movie = _movieService.GetMovieWithCategories((int)id);
            if(movie == null)
            {
                return NotFound();
            }
            return View(new MovieCategoryViewModel
            {
                Movie = movie,
                Categories = movie.MovieCategories.Select(s=>s.Category).ToList()
            });  
        }
        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Categories = new SelectList(CategoryRepository.Categories, "CategoryId", "Name");
            return View(/*new Movie()*/);
        }
        [HttpPost]
        public IActionResult Create(Movie model)
        {
            //if(ModelState.IsValid)
            //{
            //    MovieRepository.AddMovie(model);
            //    return RedirectToAction("List");
            //}
            //ViewBag.Categories = new SelectList(CategoryRepository.Categories, "CategoryId", "Name");
            return View();

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //ViewBag.Categories = new SelectList(CategoryRepository.Categories, "CategoryId", "Name");
            return View(/*MovieRepository.GetMovieById(id)*/);
        }
        [HttpPost]
        public IActionResult Edit(Movie model)
        {
            //MovieRepository.UpdateMovie(model);
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delete([FromForm] int id)
        {
            //MovieRepository.DeleteMovie(id);
            return RedirectToAction("List");
        }
    }
}
