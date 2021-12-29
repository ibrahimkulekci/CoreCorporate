using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ReferenceController : Controller
    {
        public IActionResult ReferenceList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ReferenceAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ReferenceAdd(Reference p)
        {
            return View();
        }
        [HttpGet]
        public IActionResult ReferenceUpdate(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult ReferenceUpdate(Reference p)
        {
            return View();
        }

    }
}
