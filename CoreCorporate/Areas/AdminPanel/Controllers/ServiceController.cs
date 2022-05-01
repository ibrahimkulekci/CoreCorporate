using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.Models;
using BusinessLayer.Models.Service;
using BusinessLayer.ValidationRules;
using CoreCorporate.Areas.AdminPanel.Models.Service;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin,Moderatör")]
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

        public IActionResult Index(ListViewModel model)
        {
            if (model == null)
            {
                model = new ListViewModel();
                model.CurrentPage = 1;
                model.PageSize = 10;
                model.SortOn = nameof(EntityLayer.Concrete.Service.ServiceCreatedDate);
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
                model.SortOn = nameof(EntityLayer.Concrete.Service.ServiceCreatedDate);
            }
            if (string.IsNullOrEmpty(model.SortDirection))
            {
                model.SortDirection = "desc";
            }

            BaseResultListModel<ServiceWithDetail> recordList = sm.GetAllByQuery(model);
            model.DataList = recordList.DataList;
            model.TotalRecordCount = recordList.TotalRecordCount;

            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {            
            ViewBag.categoryList = GetServiceCategoryList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Service p)
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
                return RedirectToAction("Index", "Service");
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
        public IActionResult Update(int id)
        {
            ViewBag.categoryList = GetServiceCategoryList();
            var values = sm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(Service p)
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
                return RedirectToAction("Update", new { id = p.ServiceID });
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
