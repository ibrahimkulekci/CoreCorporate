using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CatalogController : Controller
    {
        CatalogManager cm = new CatalogManager(new EfCatalogRepository(new AppDbContext()));
        CatalogValidator cv = new CatalogValidator();

        public IActionResult CatalogList()
        {
            var values = cm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CatalogAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CatalogAdd(Catalog p)
        {
            ValidationResult results = cv.Validate(p);
            if (results.IsValid)
            {
                if (p.CatalogImageFile != null)
                {
                    var extension = Path.GetExtension(p.CatalogImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.CatalogTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/CatalogImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.CatalogImageFile.CopyTo(stream);
                    p.CatalogImage = newImageName;
                }
                else
                {
                    p.CatalogImage = "nullimage.jpg";
                }
                p.CatalogCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.CatalogUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.CatalogUrl = SeoHelper.ConvertToValidUrl(p.CatalogTitle);
                cm.TAdd(p);
                return RedirectToAction("CatalogList", "Catalog");
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
        public IActionResult CatalogUpdate(int id)
        {
            var values = cm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult CatalogUpdate(Catalog p)
        {
            ValidationResult results = cv.Validate(p);
            if (results.IsValid)
            {
                if (p.CatalogImageFile != null)
                {
                    var extension = Path.GetExtension(p.CatalogImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.CatalogTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/CatalogImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.CatalogImageFile.CopyTo(stream);
                    p.CatalogImage = newImageName;
                }
                p.CatalogUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.CatalogUrl = SeoHelper.ConvertToValidUrl(p.CatalogTitle);
                cm.TUpdate(p);
                return RedirectToAction("CatalogUpdate", new { id = p.CatalogID });
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

    }
}
