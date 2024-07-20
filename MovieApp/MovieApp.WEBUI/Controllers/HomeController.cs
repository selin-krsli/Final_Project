using Microsoft.AspNetCore.Mvc;
using MovieApp.DATA.Abstract;
using MovieApp.WEBUI.ViewModels;

namespace MovieApp.WEBUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        public HomeController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public IActionResult Index()
        {
            var movieViewModel = new MovieViewModel()
            {
                Movies = _movieRepository.GetAll()
            };

            return View(movieViewModel);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
