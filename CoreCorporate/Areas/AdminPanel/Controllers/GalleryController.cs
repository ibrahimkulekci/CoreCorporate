using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class GalleryController : Controller
    {
        GalleryManager gm = new GalleryManager(new EfGalleryRepository());
        GalleryValidator gv = new GalleryValidator();

        public IActionResult GalleryList()
        {
            var values = gm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult GalleryAdd() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult GalleryAdd(Gallery p)
        {
            ValidationResult results = gv.Validate(p);
            if (results.IsValid)
            {
                /*if(p.GalleryImageFile != null)
                {
                    foreach (var item in p.GalleryImageFile)
                    {
                        p.GalleryImageFile.Count();
                        if (item.Length > 0)
                        {
                            
                            var extension = Path.GetExtension(item.FileName);
                            var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.GalleryTitle) + extension;
                            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/GalleryImages/", newImageName);
                            var stream = new FileStream(location, FileMode.Create);
                            item.CopyTo(stream);
                        }
                    }
                }*/
                if (p.GalleryImageFile != null)
                {
                    var extension = Path.GetExtension(p.GalleryImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.GalleryTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/GalleryImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.GalleryImageFile.CopyTo(stream);
                    p.GalleryImage = newImageName;
                }
                else
                {
                    p.GalleryImage = "nullimage.jpg";
                }
                p.GalleryCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.GalleryUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.GalleryUrl = SeoHelper.ConvertToValidUrl(p.GalleryTitle);
                gm.TAdd(p);
                return RedirectToAction("GalleryList", "Gallery");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult GalleryUpdate(int id)
        {
            var values = gm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult GalleryUpdate(Gallery p)
        {
            ValidationResult results = gv.Validate(p);
            if (results.IsValid)
            {
                if (p.GalleryImageFile != null)
                {
                    var extension = Path.GetExtension(p.GalleryImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.GalleryTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ServiceImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.GalleryImageFile.CopyTo(stream);
                    p.GalleryImage = newImageName;
                }
                p.GalleryUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.GalleryUrl = SeoHelper.ConvertToValidUrl(p.GalleryTitle);
                gm.TUpdate(p);
                return RedirectToAction("GalleryUpdate", new { id = p.GalleryID });
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult GalleryImageAdd(int id)
        {
            var values = gm.TGetById(id);
            return View(values);
        }
    }
}
