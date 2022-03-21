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
    public class GalleryController : Controller
    {
        private readonly IGalleryService _gs;
        private readonly IGalleryImageService _gis;

        public GalleryController(IGalleryService gs, IGalleryImageService gis)
        {
            _gs = gs;
            _gis=gis;
        }

        public IActionResult Index()
        {
            var values = _gs.GetList().Where(x => x.GalleryStatus == true).ToList();
            return View(values);
        }
        public IActionResult Details(string url)
        {
            var gallery = _gs.GetList().Where(x => x.GalleryUrl == url).FirstOrDefault();
            ViewBag.GalleryName = gallery.GalleryTitle;
            ViewBag.GalleryDesc = gallery.GalleryContent;
            var values = _gis.GetList().OrderBy(z=>z.DisplayOrder).Where(y => y.GalleryId == gallery.GalleryID).ToList();
            return View(values);
        }
    }
}
