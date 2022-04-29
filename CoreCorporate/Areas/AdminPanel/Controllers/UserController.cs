using CoreCorporate.Areas.AdminPanel.Models.User;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel p)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, false, true);
                if (result.Succeeded)
                {
                    var values = await _userManager.FindByNameAsync(p.Username);
                    var nameSurname = _userManager.Users.Where(x => x.Id == values.Id).FirstOrDefault();
                    HttpContext.Session.SetString("id", nameSurname.Id.ToString());
                    HttpContext.Session.SetString("namesurname", nameSurname.NameSurname.ToString());
                    HttpContext.Session.SetString("email", nameSurname.Email.ToString());


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Email = model.Mail,
                    UserName = model.Username,
                    NameSurname = model.NameSurname
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
