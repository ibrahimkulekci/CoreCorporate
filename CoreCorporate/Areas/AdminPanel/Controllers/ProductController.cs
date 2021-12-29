using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        ProductManager pm = new ProductManager(new EfProductRepository());
        ProductValidator pv = new ProductValidator();

        ProductCategoryManager pcm = new ProductCategoryManager(new EfProductCategoryRepository());

        public List<SelectListItem> GetProductCategoryList()
        {
            List<SelectListItem> categoryList = (from x in pcm.GetList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.ProductCategoryTitle,
                                                     Value = x.ProductCategoryID.ToString()
                                                 }).ToList();
            return categoryList;
        }

        public IActionResult ProductList()
        {
            var values = pm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult ProductAdd()
        {
            ViewBag.categoryList = GetProductCategoryList();
            return View();
        }
        [HttpPost]
        public IActionResult ProductAdd(Product p)
        {
            ValidationResult results = pv.Validate(p);
            if (results.IsValid)
            {
                if (p.ProductImageFile != null)
                {
                    var extension = Path.GetExtension(p.ProductImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.ProductTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ProductImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.ProductImageFile.CopyTo(stream);
                    p.ProductImage = newImageName;
                }
                else
                {
                    p.ProductImage = "nullimage.jpg";
                }
                p.ProductCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ProductUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ProductUrl = SeoHelper.ConvertToValidUrl(p.ProductTitle);
                pm.TAdd(p);
                return RedirectToAction("ProductList", "Product");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            ViewBag.categoryList = GetProductCategoryList();
            return View();
        }
        [HttpGet]
        public IActionResult ProductUpdate(int id)
        {
            ViewBag.categoryList = GetProductCategoryList();
            var values = pm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult ProductUpdate(Product p)
        {
            ValidationResult results = pv.Validate(p);
            if (results.IsValid)
            {
                if (p.ProductImageFile != null)
                {
                    var extension = Path.GetExtension(p.ProductImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.ProductTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ProductImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.ProductImageFile.CopyTo(stream);
                    p.ProductImage = newImageName;
                }
                p.ProductUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ProductUrl = SeoHelper.ConvertToValidUrl(p.ProductTitle);
                pm.TUpdate(p);
                return RedirectToAction("ProductUpdate", new { id = p.ProductID });
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            ViewBag.categoryList = GetProductCategoryList();
            return View();
        }



    }
}
