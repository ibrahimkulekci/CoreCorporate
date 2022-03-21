using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Controllers
{
    [AllowAnonymous]
    public class PageController : Controller
    {
        PageManager pm = new PageManager(new EfPageRepository(new AppDbContext()));

        AppDbContext c = new AppDbContext();

        public IActionResult Index(string p)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Details(string url)
        {
            var values = c.Pages.FirstOrDefault(x => x.PageUrl == url);
            return View(values);
        }
    }
}
