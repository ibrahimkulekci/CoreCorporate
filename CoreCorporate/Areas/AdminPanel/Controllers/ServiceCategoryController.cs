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
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ServiceCategoryController : Controller
    {
        ServiceCategoryManager scm = new ServiceCategoryManager(new EfServiceCategoryRepository(new AppDbContext()));
        ServiceCategoryValidator scv = new ServiceCategoryValidator();

        public IActionResult ServiceCategoryList()
        {
            var values = scm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult ServiceCategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ServiceCategoryAdd(ServiceCategory p)
        {
            ValidationResult results = scv.Validate(p);
            if (results.IsValid)
            {
                p.ServiceCategoryCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ServiceCategoryUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ServiceCategoryUrl = SeoHelper.ConvertToValidUrl(p.ServiceCategoryTitle);
                scm.TAdd(p);
                return RedirectToAction("ServiceCategoryList", "ServiceCategory");
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
        public IActionResult ServiceCategoryUpdate(int id)
        {
            var values = scm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult ServiceCategoryUpdate(ServiceCategory p)
        {
            ValidationResult results = scv.Validate(p);
            if (results.IsValid)
            {
                p.ServiceCategoryUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ServiceCategoryUrl = SeoHelper.ConvertToValidUrl(p.ServiceCategoryTitle);
                scm.TUpdate(p);
                return RedirectToAction("ServiceCategoryUpdate", new { id = p.ServiceCategoryID });
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
