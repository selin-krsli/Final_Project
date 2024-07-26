using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BUSINESS.Abstract;
using MovieApp.ENTITY;
using MovieApp.WEBUI.Identity;
using MovieApp.WEBUI.Models;
using Newtonsoft.Json;

namespace MovieApp.WEBUI.Controllers
{
    [Authorize(Roles ="admin")]
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICategoryService _categoryService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public AdminController(IMovieService movieService, ICategoryService categoryService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _movieService = movieService;
            _categoryService = categoryService;
            _roleManager = roleManager;
            _userManager = userManager;

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
            if (ModelState.IsValid)
            {
                var movieEntity = new Movie
                {
                    MovieName = model.MovieName,
                    Url = model.Url,
                    MovieStory = model.MovieStory,
                    Genre = model.Genre,
                    Director = model.Director,
                    Image = model.Image,
                    Price = model.Price ?? 0,
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
            return View(model);
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
            //if (ModelState.IsValid)
            //{
                var movieEntity = _movieService.GetById(model.MovieId);
                if (movieEntity == null)
                {
                    return NotFound();
                }
                movieEntity.MovieName = model.MovieName;
                movieEntity.Url = model.Url;
                movieEntity.Price = model.Price ?? 0;
                movieEntity.MovieStory = model.MovieStory;
                movieEntity.Genre = model.Genre;
                movieEntity.Director = model.Director;
                movieEntity.Image = model.Image;

                _movieService.Update(movieEntity, categoryIds);

                var serializeObj = new MessageBoxInfo
                {
                    Message = $"{movieEntity.MovieName} is updated!",
                    AlertType = "warning"
                };

                TempData["message"] = JsonConvert.SerializeObject(serializeObj);
                return RedirectToAction("MovieList");
            //}
           // ViewBag.Categories = _categoryService.GetAll();
            //return View(model);
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
            //if(ModelState.IsValid)
            //{
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
            //}
            //return View(model);
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
            //if(ModelState.IsValid)
            //{
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
            //}
            //return View(model);
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
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if(result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if(string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var members = new List<User>();
            var nonMembers = new List<User>();
            foreach (var user in _userManager.Users.ToList())
            {
                var list = await _userManager.IsInRoleAsync(user,role.Name) ? members : nonMembers;
                list.Add(user);
            }
            var model = new RoleDetails
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditModel model)
        {
            if(ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] {})
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if(user!=null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if(!result.Succeeded)
                        {
                            foreach (var err in result.Errors)
                            {
                                ModelState.AddModelError("", err.Description);
                            }
                        }
                    }
                }
                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var err in result.Errors)
                            {
                                ModelState.AddModelError("", err.Description);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("RoleList");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(s => s.Name);

                ViewBag.Roles = roles;
                return View(new UserDetailsModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.Name,
                    LastName = user.Surname,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles,
                });
            }
            return RedirectToAction("UserList", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(UserDetailsModel model, string[] selectedRoles)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if(user != null)
                {
                    user.Name = model.FirstName;
                    user.Surname = model.LastName;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result = await _userManager.UpdateAsync(user);
                    if(result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());

                        return RedirectToAction("UserList", "Admin");
                    }
                }
                return RedirectToAction("UserList", "Admin");
            }
            return View(model);
        }

    }
}
