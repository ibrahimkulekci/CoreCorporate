using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ServiceController : Controller
    {
        ServiceManager sm = new ServiceManager(new EfServiceRepository(new AppDbContext()));
        ServiceCategoryManager scm = new ServiceCategoryManager(new EfServiceCategoryRepository(new AppDbContext()));
        ServiceValidator sv = new ServiceValidator();

        public List<SelectListItem> GetServiceCategoryList()
        {
            List<SelectListItem> categoryList = (from x in scm.GetList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.ServiceCategoryTitle,
                                                     Value = x.ServiceCategoryID.ToString()
                                                 }).ToList();
            return categoryList;
        }

        public IActionResult ServiceList()
        {
            var values = sm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult ServiceAdd()
        {            
            ViewBag.categoryList = GetServiceCategoryList();
            return View();
        }
        [HttpPost]
        public IActionResult ServiceAdd(Service p)
        {
            ValidationResult results = sv.Validate(p);
            if (results.IsValid)
            {
                if (p.ServiceImageFile != null)
                {
                    var extension = Path.GetExtension(p.ServiceImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.ServiceTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ServiceImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.ServiceImageFile.CopyTo(stream);
                    p.ServiceImage = newImageName;
                }
                else
                {
                    p.ServiceImage = "nullimage.jpg";
                }
                p.ServiceCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ServiceUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ServiceUrl = SeoHelper.ConvertToValidUrl(p.ServiceTitle);
                sm.TAdd(p);
                return RedirectToAction("ServiceList", "Service");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            ViewBag.categoryList = GetServiceCategoryList();
            return View();
        }
        [HttpGet]
        public IActionResult ServiceUpdate(int id)
        {
            ViewBag.categoryList = GetServiceCategoryList();
            var values = sm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult ServiceUpdate(Service p)
        {
            ValidationResult results = sv.Validate(p);
            if (results.IsValid)
            {
                if (p.ServiceImageFile != null)
                {
                    var extension = Path.GetExtension(p.ServiceImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.ServiceTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ServiceImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.ServiceImageFile.CopyTo(stream);
                    p.ServiceImage = newImageName;
                }
                p.ServiceUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ServiceUrl = SeoHelper.ConvertToValidUrl(p.ServiceTitle);
                sm.TUpdate(p);
                return RedirectToAction("ServiceUpdate", new { id = p.ServiceID });
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            ViewBag.categoryList = GetServiceCategoryList();
            return View();
        }

    }
}
