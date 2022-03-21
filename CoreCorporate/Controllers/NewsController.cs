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
    public class NewsController : Controller
    {
        private readonly INewsService _ns;

        public NewsController(INewsService ns)
        {
            _ns = ns;
        }

        public IActionResult Index()
        {
            var values = _ns.GetList().Where(x => x.NewsStatus == true).Take(10).ToList();
            return View(values);
        }
        public IActionResult Details(string url)
        {
            var values = _ns.GetList().Where(x => x.NewsUrl == url).FirstOrDefault();
            return View(values);
        }
    }
}
