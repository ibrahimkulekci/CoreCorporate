using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CatalogController : Controller
    {
        public IActionResult CatalogList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CatalogAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CatalogAdd(Catalog p)
        {
            return View();
        }
        [HttpGet]
        public IActionResult CatalogUpdate(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult CatalogUpdate(Catalog p)
        {
            return View();
        }

    }
}
