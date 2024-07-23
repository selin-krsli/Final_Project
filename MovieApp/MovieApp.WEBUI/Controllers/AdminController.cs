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
        private readonly ICategoryService _categoryService;
        public AdminController(IMovieService movieService, ICategoryService categoryService)
        {
            _movieService = movieService;
            _categoryService = categoryService;

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
                var movieEntity = _movieService.GetByIdWithCategories((int)id);
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
                    Image = movieEntity.Image,
                    SelectedCategories = movieEntity.MovieCategories.Select(c=>c.Category).ToList()
                };

                ViewBag.Categories = _categoryService.GetAll();
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult EditMovie(MovieModel model, int[] categoryIds)
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

            _movieService.Update(movieEntity, categoryIds);

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
        public IActionResult CategoryList()
        {
            var categoryListViewModel = new CategoryListViewModel
            {
                Categories = _categoryService.GetAll()
            };
            return View(categoryListViewModel);
        }
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            var categoryEntity = new Category
            {
                 Name = model.Name,
                 Url = model.Url,
            };
            _categoryService.Create(categoryEntity);
            var serializeObj = new MessageBoxInfo
            {
                Message = $"{categoryEntity.Name} is added!",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(serializeObj);
            return RedirectToAction("CategoryList");
        }
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var categoryEntity = _categoryService.GetByIdWithMovies((int)id);
                if (categoryEntity == null)
                {
                    return NotFound();
                }
                var model = new CategoryModel
                {
                    CategoryId = categoryEntity.CategoryId,
                    Name = categoryEntity.Name,
                    Url = categoryEntity.Url,
                    Movies = categoryEntity.MovieCategories.Select(m=>m.Movie).ToList()
                };
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var categoryEntity = _categoryService.GetById(model.CategoryId);
            if (categoryEntity == null)
            {
                return NotFound();
            }
            categoryEntity.Name = model.Name;
            categoryEntity.Url = model.Url;

            _categoryService.Update(categoryEntity);

            var serializeObj = new MessageBoxInfo
            {
                Message = $"{categoryEntity.Name} is updated!",
                AlertType = "warning"
            };

            TempData["message"] = JsonConvert.SerializeObject(serializeObj);

            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            var categoryEntity = _categoryService.GetById(categoryId);
            if(categoryEntity != null)
            {
                _categoryService.Delete(categoryEntity);
            }
            var serializeObj = new MessageBoxInfo
            {
                Message = $"{categoryEntity.Name} is deleted!",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(serializeObj);
            return RedirectToAction("CategoryList");
        }

    }
}
