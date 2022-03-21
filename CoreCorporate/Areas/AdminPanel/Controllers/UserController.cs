using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User p)
        {
            AppDbContext c = new AppDbContext();
            var dataValue = c.Users.FirstOrDefault(x => x.Username == p.Username && x.Password == p.Password);
            if (dataValue != null)
            {
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,dataValue.Username)
                };
                var useridentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                
                HttpContext.Session.SetString("namesurname", dataValue.NameSurname);
                HttpContext.Session.SetString("profileimage", dataValue.ProfileImage);
                HttpContext.Session.SetString("mail", dataValue.Mail);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }            
        }

    }
}
