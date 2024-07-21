using Microsoft.AspNetCore.Mvc;
using MovieApp.BUSINESS.Abstract;
using MovieApp.WEBUI.ViewModels;

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
                Movies = _movieService.GetAll()
            };

            return View(movieViewModel);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
