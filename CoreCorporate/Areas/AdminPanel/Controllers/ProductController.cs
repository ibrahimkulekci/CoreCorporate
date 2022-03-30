using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.Models;
using BusinessLayer.Models.Product;
using BusinessLayer.ValidationRules;
using CoreCorporate.Areas.AdminPanel.Models.Product;
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
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        ProductManager pm = new ProductManager(new EfProductRepository(new AppDbContext()));
        ProductValidator pv = new ProductValidator();

        ProductCategoryManager pcm = new ProductCategoryManager(new EfProductCategoryRepository(new AppDbContext()));

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

        public IActionResult Index(ListViewModel model) 
        {
            if (model == null)
            {
                model = new ListViewModel();
                model.CurrentPage = 1;
                model.PageSize = 10;
                model.SortOn = nameof(EntityLayer.Concrete.Product.ProductCreatedDate);
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
                model.SortOn = nameof(EntityLayer.Concrete.Product.ProductCreatedDate);
            }

            if (string.IsNullOrEmpty(model.SortDirection))
            {
                model.SortDirection = "desc";
            }

            BaseResultListModel<ProductWithDetail> recordList = pm.GetAllByQuery(model);
            model.DataList = recordList.DataList;
            model.TotalRecordCount = recordList.TotalRecordCount;

            return View(model);


        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.categoryList = GetProductCategoryList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product p)
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
                return RedirectToAction("Index", "Product");
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
        public IActionResult Update(int id)
        {
            ViewBag.categoryList = GetProductCategoryList();
            var values = pm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(Product p)
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
                return RedirectToAction("Update", new { id = p.ProductID });
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
