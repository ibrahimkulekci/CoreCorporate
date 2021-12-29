using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class PartnerController : Controller
    {
        public IActionResult PartnerList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PartnerAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PartnerAdd(Partner p)
        {
            return View();
        }
        [HttpGet]
        public IActionResult PartnerUpdate(int id)
        {
            return View();
        } 
        [HttpPost]
        public IActionResult PartnerUpdate(Partner p)
        {
            return View();
        }
    }
}
