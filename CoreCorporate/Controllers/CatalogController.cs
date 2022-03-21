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
    public class CatalogController : Controller
    {
        private readonly ICatalogService _cm;

        public CatalogController(ICatalogService cm)
        {
            _cm = cm;
        }        

        public IActionResult Index()
        {
            var values = _cm.GetListBySatusTrue();
            return View(values);
        }

        public IActionResult Details(string url)
        {
            var values = _cm.GetList().Where(x=>x.CatalogUrl==url).FirstOrDefault();
            return View(values);
        }
    }
}
