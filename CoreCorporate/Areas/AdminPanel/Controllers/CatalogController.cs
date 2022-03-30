using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.Models;
using BusinessLayer.ValidationRules;
using CoreCorporate.Areas.AdminPanel.Models.Catalog;
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

        public IActionResult Index(ListViewModel model)
        {
            if (model == null)
            {
                model = new ListViewModel();
                model.CurrentPage = 1;
                model.PageSize = 10;
                model.SortOn = nameof(EntityLayer.Concrete.Catalog.CatalogCreatedDate);
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
                model.SortOn = nameof(EntityLayer.Concrete.Catalog.CatalogCreatedDate);
            }

            if (string.IsNullOrEmpty(model.SortDirection))
            {
                model.SortDirection = "desc";
            }

            BaseResultListModel<EntityLayer.Concrete.Catalog> recordList = cm.GetAllByQuery(model);

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
        public IActionResult Add(Catalog p)
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
                return RedirectToAction("Index", "Catalog");
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
            var values = cm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(Catalog p)
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
                return RedirectToAction("Update", new { id = p.CatalogID });
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
