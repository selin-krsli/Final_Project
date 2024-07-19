using Microsoft.AspNetCore.Mvc;
using MovieApp.WEBUI.Data;
using MovieApp.WEBUI.Models;
using MovieApp.WEBUI.ViewModels;

namespace MovieApp.WEBUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var movieViewModel = new MovieViewModel {Movies = MovieRepository.Movies };

            return View(movieViewModel);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
