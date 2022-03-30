using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.Models;
using BusinessLayer.ValidationRules;
using CoreCorporate.Areas.AdminPanel.Models.ServiceCategory;
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


        public IActionResult Index(ListViewModel model)
        {
            if (model == null)
            {
                model = new ListViewModel();
                model.CurrentPage = 1;
                model.PageSize = 10;
                model.SortOn = nameof(EntityLayer.Concrete.ServiceCategory.ServiceCategoryCreatedDate);
                model.SortDirection = "desc";
            }

            if (model.CurrentPage == 0)
            {
                model.CurrentPage = 1;
            }

            if (model.PageSize == 0)
            {
                model.PageSize = 10;
            }

            if (string.IsNullOrEmpty(model.SortOn))
            {
                model.SortOn = nameof(EntityLayer.Concrete.ServiceCategory.ServiceCategoryCreatedDate);
            }

            if (string.IsNullOrEmpty(model.SortDirection))
            {
                model.SortDirection = "desc";
            }

            BaseResultListModel<ServiceCategory> recordList = scm.GetAllByQuery(model);
            model.DataList = recordList.DataList;
            model.TotalRecordCount = recordList.TotalRecordCount;

            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ServiceCategory p)
        {
            ValidationResult results = scv.Validate(p);
            if (results.IsValid)
            {
                p.ServiceCategoryCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ServiceCategoryUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ServiceCategoryUrl = SeoHelper.ConvertToValidUrl(p.ServiceCategoryTitle);
                scm.TAdd(p);                
                return RedirectToAction("Index", "ServiceCategory");
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
        public IActionResult Update(int id)
        {
            var values = scm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(ServiceCategory p)
        {
            ValidationResult results = scv.Validate(p);
            if (results.IsValid)
            {
                p.ServiceCategoryUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ServiceCategoryUrl = SeoHelper.ConvertToValidUrl(p.ServiceCategoryTitle);
                scm.TUpdate(p);
                return RedirectToAction("Update", new { id = p.ServiceCategoryID });
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
