using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Controllers
{
    [AllowAnonymous]
    public class PartnerController : Controller
    {
        private readonly IPartnerService _ps;

        public PartnerController(IPartnerService ps)
        {
            _ps = ps;
        }

        public IActionResult Index()
        {
            var values = _ps.GetList().Where(x => x.PartnerStatus == true).ToList();
            return View(values);
        }
        public IActionResult Details(string url)
        {
            var values = _ps.GetList().Where(x => x.PartnerUrl == url).FirstOrDefault();
            return View(values);
        }
    }
}
