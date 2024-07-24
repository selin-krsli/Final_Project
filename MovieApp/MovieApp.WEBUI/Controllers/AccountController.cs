﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.WEBUI.Identity;
using MovieApp.WEBUI.Models;

namespace MovieApp.WEBUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;   
            _signInManager = signInManager;
        }
        public IActionResult Login(/*string ReturnUrl=null*/)
        {
            return View();
            //return View(new LoginModel
            //{
            //    ReturnUrl = ReturnUrl,
            //});
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                ModelState.AddModelError("", "No account has been created with this username before.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if(result.Succeeded)
            {
                return RedirectToAction("List", "Movie");
                //return Redirect(model.ReturnUrl??"~/");
            }
            ModelState.AddModelError("", "The entered email or password was entered incorrectly.");
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User
            {
                Name = model.FirstName,
                Surname = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError("", "An unknown error occurred, please try again.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); //tarayıcıdan cookie siliniyor.
            return RedirectToAction("Index", "Home");
        }
    }
}