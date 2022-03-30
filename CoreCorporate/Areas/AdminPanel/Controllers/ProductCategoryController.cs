using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.Models;
using BusinessLayer.ValidationRules;
using CoreCorporate.Areas.AdminPanel.Models.ProductCategory;
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
    public class ProductCategoryController : Controller
    {
        ProductCategoryManager pcm = new ProductCategoryManager(new EfProductCategoryRepository(new AppDbContext()));
        ProductCategoryValidator pcv = new ProductCategoryValidator();

        public IActionResult Index(ListViewModel model)
        {
            if (model == null)
            {
                model = new ListViewModel();
                model.CurrentPage = 1;
                model.PageSize = 10;
                model.SortOn = nameof(EntityLayer.Concrete.ProductCategory.ProductCategoryCreatedDate);
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
                model.SortOn = nameof(EntityLayer.Concrete.ProductCategory.ProductCategoryCreatedDate);
            }

            if (string.IsNullOrEmpty(model.SortDirection))
            {
                model.SortDirection = "desc";
            }

            BaseResultListModel<EntityLayer.Concrete.ProductCategory> recordList = pcm.GetAllByQuery(model);

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
        public IActionResult Add(ProductCategory p)
        {
            ValidationResult results = pcv.Validate(p);
            if (results.IsValid)
            {
                if (p.ProductCategoryImageFile != null)
                {
                    var extension = Path.GetExtension(p.ProductCategoryImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.ProductCategoryTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ProductCategoryImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.ProductCategoryImageFile.CopyTo(stream);
                    p.ProductCategoryImage = newImageName;
                }
                else
                {
                    p.ProductCategoryImage = "nullimage.jpg";
                }

                p.ProductCategoryCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ProductCategoryUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ProductCategoryUrl = SeoHelper.ConvertToValidUrl(p.ProductCategoryTitle);
                pcm.TAdd(p);
                return RedirectToAction("Index", "ProductCategory");
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
            var values = pcm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(ProductCategory p)
        {
            ValidationResult results = pcv.Validate(p);
            if (results.IsValid)
            {
                if (p.ProductCategoryImageFile != null)
                {
                    var extension = Path.GetExtension(p.ProductCategoryImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.ProductCategoryTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ProductCategoryImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.ProductCategoryImageFile.CopyTo(stream);
                    p.ProductCategoryImage = newImageName;
                }
                p.ProductCategoryUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ProductCategoryUrl = SeoHelper.ConvertToValidUrl(p.ProductCategoryTitle);
                pcm.TUpdate(p);
                return RedirectToAction("Update", new { id = p.ProductCategoryID });
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
