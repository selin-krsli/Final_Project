using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.BUSINESS.Abstract;
using MovieApp.ENTITY;
using MovieApp.WEBUI.Models;

namespace MovieApp.WEBUI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult List(string category, int page=1)
        {
            const int pageSize = 6;
            var movieListViewModel = new MovieListViewModel
            {
                PageInfo = new PageInfo
                {
                    TotalItems = _movieService.GetMovieByCategory(category),
                    CurrentPageIndex = page,
                    ItemsPerPage = pageSize,
                    CurrentCategory = category
                },
                Movies = _movieService.GetMovieWithCategories(category,page, pageSize)
            };
            return View(movieListViewModel);
        }
        public IActionResult Details(string url)
        {
            if(url == null)
            {
                return NotFound();
            }
            var movie = _movieService.GetMovieDetails(url);
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
            return View();
        }
        [HttpPost]
        public IActionResult Create(Movie model)
        {
            return View();

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //ViewBag.Categories = new SelectList(CategoryRepository.Categories, "CategoryId", "Name");
            return View();
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
        public IActionResult Search(string searchString)
        {
            var movieListViewModel = new MovieListViewModel
            {
                Movies = _movieService.GetSearchResult(searchString)
            };
            return View(movieListViewModel);
        }
    }
}
