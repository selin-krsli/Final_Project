using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.WEBUI.Data;
using MovieApp.WEBUI.Models;
using MovieApp.WEBUI.ViewModels;

namespace MovieApp.WEBUI.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            var movie = new Movie { Name="Iphone X", Price= 5000, Description="Mükoo"};

            return View(movie);
        }
        //id null ise; /movie/list --> Tüm filmleri listele
        //id null değilse; /movie/list/2 --> 2 numaralı kategoride var olan filmleri listele.
        public IActionResult List(int? id, string searchString)
        {
            var movies = MovieRepository.Movies;

            if(id != null)
            {
                movies = movies.Where(m=>m.CategoryId == id).ToList();
            }
            if(!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s=>s.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            var movieViewModel = new MovieViewModel { Movies = movies };
            
            return View(movieViewModel);
        }
        public IActionResult Details(int id)
        {
            return View(MovieRepository.GetMovieById(id));  
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(CategoryRepository.Categories, "CategoryId", "Name");
            return View(new Movie());
        }
        [HttpPost]
        public IActionResult Create(Movie model)
        {
            if(ModelState.IsValid)
            {
                MovieRepository.AddMovie(model);
                return RedirectToAction("List");
            }
            ViewBag.Categories = new SelectList(CategoryRepository.Categories, "CategoryId", "Name");
            return View(model);

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(CategoryRepository.Categories, "CategoryId", "Name");
            return View(MovieRepository.GetMovieById(id));
        }
        [HttpPost]
        public IActionResult Edit(Movie model)
        {
            MovieRepository.UpdateMovie(model);
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delete([FromForm] int id)
        {
            MovieRepository.DeleteMovie(id);
            return RedirectToAction("List");
        }
    }
}
