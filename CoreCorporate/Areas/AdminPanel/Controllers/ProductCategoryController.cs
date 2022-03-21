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
    public class ProductCategoryController : Controller
    {
        ProductCategoryManager pcm = new ProductCategoryManager(new EfProductCategoryRepository(new AppDbContext()));
        ProductCategoryValidator pcv = new ProductCategoryValidator();

        public IActionResult ProductCategoryList()
        {
            var values = pcm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult ProductCategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ProductCategoryAdd(ProductCategory p)
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
                return RedirectToAction("ProductCategoryList", "ProductCategory");
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
        public IActionResult ProductCategoryUpdate(int id)
        {
            var values = pcm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult ProductCategoryUpdate(ProductCategory p)
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
                return RedirectToAction("ProductCategoryUpdate", new { id = p.ProductCategoryID });
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
