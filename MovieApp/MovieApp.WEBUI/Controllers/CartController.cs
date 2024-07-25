using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BUSINESS.Abstract;
using MovieApp.WEBUI.Identity;
using MovieApp.WEBUI.Models;

namespace MovieApp.WEBUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private UserManager<User> _userManager;
        public CartController(ICartService cartService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var cartInfo = _cartService.GetCartByUserId(_userManager.GetUserId(User));
            return View(new CartModel
            {
                CartId = cartInfo.Id,
                CartItems = cartInfo.CartItems.Select(s=> new CartItemModel
                {
                     CartItemId = s.Id,
                     Name = s.Movie.MovieName,
                     Price = (double)s.Movie.Price,
                     Image = s.Movie.Image,
                     MovieId = s.MovieId,
                     Quantity = s.Quantity
                }).ToList()
            });
        }
        [HttpPost]
        public IActionResult AddToCart(int movieId, int quantity)
        {
            var userId = _userManager.GetUserId(User);  
            _cartService.AddToCart(userId,movieId, quantity);
            return RedirectToAction("Index");   
        }
        [HttpPost]
        public IActionResult DeleteFromCart(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.DeleteFromCart(userId, movieId);
            return RedirectToAction("Index");   
        }
    }
}
