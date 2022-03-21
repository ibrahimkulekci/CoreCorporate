using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeController : Controller
    {
        AppDbContext con = new AppDbContext();

        public IActionResult Index()
        {
            ViewBag.PageCount = con.Pages.Count();
            ViewBag.ServiceCount = con.Services.Count();
            ViewBag.NewsCount = con.News.Count();
            ViewBag.ProductCount = con.Products.Count();
            ViewBag.GalleryCount = con.Galleries.Count();
            ViewBag.PartnerCount = con.Partners.Count();
            ViewBag.ReferenceCount = con.References.Count();
            ViewBag.CatalogCount = con.Catalogs.Count();
            ViewBag.TeamCount = con.Teams.Count();

            return View();
        }
    }
}
