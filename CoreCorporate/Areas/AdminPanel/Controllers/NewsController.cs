using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
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
    public class NewsController : Controller
    {
        NewsManager nm = new NewsManager(new EfNewsRepository());
        NewsValidator nv = new NewsValidator();

        public IActionResult NewsList()
        {
            var values = nm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult NewsAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewsAdd(News p)
        {
            ValidationResult results = nv.Validate(p);
            if (results.IsValid)
            {
                if (p.NewsImageFile != null)
                {
                    var extension = Path.GetExtension(p.NewsImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.NewsTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/NewsImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.NewsImageFile.CopyTo(stream);
                    p.NewsImage = newImageName;
                }
                else
                {
                    p.NewsImage = "nullimage.jpg";
                }
                p.NewsCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.NewsUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.NewsUrl = SeoHelper.ConvertToValidUrl(p.NewsTitle);
                nm.TAdd(p);
                return RedirectToAction("NewsList", "News");
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
        public IActionResult NewsUpdate(int id)
        {
            var values = nm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult NewsUpdate(News p)
        {
            ValidationResult results = nv.Validate(p);
            if (results.IsValid)
            {
                if (p.NewsImageFile != null)
                {
                    var extension = Path.GetExtension(p.NewsImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.NewsTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/NewsImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.NewsImageFile.CopyTo(stream);
                    p.NewsImage = newImageName;
                }
                p.NewsUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.NewsUrl = SeoHelper.ConvertToValidUrl(p.NewsTitle);
                nm.TUpdate(p);
                return RedirectToAction("NewsUpdate", new { id = p.NewsID });
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
