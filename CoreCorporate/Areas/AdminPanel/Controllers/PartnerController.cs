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
    public class PartnerController : Controller
    {
        PartnerManager pm = new PartnerManager(new EfPartnerRepository(new AppDbContext()));
        PartnerValidator pv = new PartnerValidator();

        public IActionResult PartnerList()
        {
            var values = pm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult PartnerAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PartnerAdd(Partner p)
        {
            ValidationResult results = pv.Validate(p);
            if (results.IsValid)
            {
                if (p.PartnerImageFile != null)
                {
                    var extension = Path.GetExtension(p.PartnerImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.PartnerTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/PartnerImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.PartnerImageFile.CopyTo(stream);
                    p.PartnerImage = newImageName;
                }
                else
                {
                    p.PartnerImage = "nullimage.jpg";
                }
                p.PartnerCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PartnerUpdateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PartnerUrl = SeoHelper.ConvertToValidUrl(p.PartnerTitle);
                pm.TAdd(p);
                return RedirectToAction("PartnerList", "Partner");
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
        public IActionResult PartnerUpdate(int id)
        {
            var values = pm.TGetById(id);
            return View(values);
        } 
        [HttpPost]
        public IActionResult PartnerUpdate(Partner p)
        {
            ValidationResult results = pv.Validate(p);
            if (results.IsValid)
            {
                if (p.PartnerImageFile != null)
                {
                    var extension = Path.GetExtension(p.PartnerImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.PartnerTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/PartnerImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.PartnerImageFile.CopyTo(stream);
                    p.PartnerImage = newImageName;
                }
                p.PartnerUpdateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PartnerUrl = SeoHelper.ConvertToValidUrl(p.PartnerTitle);
                pm.TUpdate(p);
                return RedirectToAction("PartnerUpdate", new { id = p.PartnerID });
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
