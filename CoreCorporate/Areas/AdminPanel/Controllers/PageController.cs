using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
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
    public class PageController : Controller
    {
        PageManager pm = new PageManager(new EfPageRepository());
        PageValidator pv = new PageValidator();

        public IActionResult PageList()
        {
            var values = pm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult PageAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PageAdd(Page p)
        {
            ValidationResult results = pv.Validate(p);
            if (results.IsValid)
            {
                p.PageCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PageStatus = p.PageStatus;
                p.PageUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PageUrl = SeoHelper.ConvertToValidUrl(p.PageTitle);
                pm.TAdd(p);
                return RedirectToAction("PageList", "Page");
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
        public IActionResult PageUpdate(int id)
        {
            var values = pm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult PageUpdate(Page p)
        {
            ValidationResult results = pv.Validate(p);
            if (results.IsValid)
            {
                p.PageStatus = p.PageStatus;
                p.PageUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PageUrl = SeoHelper.ConvertToValidUrl(p.PageTitle);
                pm.TUpdate(p);

                return RedirectToAction("PageUpdate", new { id = p.PageID });
            }
            return View();
        }
    }
}
