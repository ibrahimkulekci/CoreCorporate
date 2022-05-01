using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.Models;
using BusinessLayer.ValidationRules;
using CoreCorporate.Areas.AdminPanel.Models.News;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin,Moderatör,Yazar")]
    public class NewsController : Controller
    {
        NewsManager nm = new NewsManager(new EfNewsRepository(new AppDbContext()));
        NewsValidator nv = new NewsValidator();

        public IActionResult Index(ListViewModel model)
        {
            if (model == null)
            {
                model = new ListViewModel();
                model.CurrentPage = 1;
                model.PageSize = 10;
                model.SortOn = nameof(EntityLayer.Concrete.News.NewsCreatedDate);
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
                model.SortOn = nameof(EntityLayer.Concrete.News.NewsCreatedDate);
            }

            if (string.IsNullOrEmpty(model.SortDirection))
            {
                model.SortDirection = "desc";
            }
            BaseResultListModel<News> recordList = nm.GetAllByQuery(model);
            model.DataList = recordList.DataList;
            model.TotalRecordCount = recordList.TotalRecordCount;

            return View(model);
        }
        public IActionResult NewsList()
        {
            var values = nm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(News p)
        {
            ValidationResult results = nv.Validate(p);
            if (results.IsValid)
            {
                if (p.NewsImageFile != null)
                {
                    var extension = Path.GetExtension(p.NewsImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.NewsTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/NewsImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.NewsImageFile.CopyTo(stream);
                    p.NewsImage = newImageName;
                }
                else
                {
                    p.NewsImage = "nullimage.jpg";
                }
                p.NewsCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.NewsUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.NewsUrl = SeoHelper.ConvertToValidUrl(p.NewsTitle);
                nm.TAdd(p);
                return RedirectToAction("Index", "News");
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
            var values = nm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(News p)
        {
            ValidationResult results = nv.Validate(p);
            if (results.IsValid)
            {
                if (p.NewsImageFile != null)
                {
                    var extension = Path.GetExtension(p.NewsImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.NewsTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/NewsImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.NewsImageFile.CopyTo(stream);
                    p.NewsImage = newImageName;
                }
                p.NewsUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.NewsUrl = SeoHelper.ConvertToValidUrl(p.NewsTitle);
                nm.TUpdate(p);
                return RedirectToAction("Update", new { id = p.NewsID });
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
