using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class TeamController : Controller
    {
        public IActionResult TeamList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult TeamAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TeamAdd(Team p)
        {
            return View();
        }
        [HttpGet]
        public IActionResult TeamUpdate(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult TeamUpdate(Team p)
        {
            return View();
        }
    }
}
