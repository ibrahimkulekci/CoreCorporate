using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.Models;
using BusinessLayer.ValidationRules;
using CoreCorporate.Areas.AdminPanel.Models.Reference;
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
    [Authorize(Roles = "Admin,Moderatör")]
    public class ReferenceController : Controller
    {
        ReferenceManager rm = new ReferenceManager(new EfReferenceRepository(new AppDbContext()));
        ReferenceValidator rv = new ReferenceValidator();

        public IActionResult Index(ListViewModel model)
        {
            if (model == null)
            {
                model = new ListViewModel();
                model.CurrentPage = 1;
                model.PageSize = 10;
                model.SortOn = nameof(EntityLayer.Concrete.Reference.ReferenceCreatedDate);
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
                model.SortOn = nameof(EntityLayer.Concrete.Reference.ReferenceCreatedDate);
            }

            if (string.IsNullOrEmpty(model.SortDirection))
            {
                model.SortDirection = "desc";
            }

            BaseResultListModel<EntityLayer.Concrete.Reference> recordList = rm.GetAllByQuery(model);

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
        public IActionResult Add(Reference p)
        {
            ValidationResult results = rv.Validate(p);
            if (results.IsValid)
            {
                if (p.ReferenceImageFile != null)
                {
                    var extension = Path.GetExtension(p.ReferenceImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.ReferenceTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ReferenceImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.ReferenceImageFile.CopyTo(stream);
                    p.ReferenceImage = newImageName;
                }
                else
                {
                    p.ReferenceImage = "nullimage.jpg";
                }
                p.ReferenceCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ReferenceUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ReferenceUrl = SeoHelper.ConvertToValidUrl(p.ReferenceTitle);
                rm.TAdd(p);
                return RedirectToAction("Index", "Reference");
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
            var values = rm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(Reference p)
        {
            ValidationResult results = rv.Validate(p);
            if (results.IsValid)
            {
                if (p.ReferenceImageFile != null)
                {
                    var extension = Path.GetExtension(p.ReferenceImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.ReferenceTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ReferenceImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.ReferenceImageFile.CopyTo(stream);
                    p.ReferenceImage = newImageName;
                }
                p.ReferenceUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ReferenceUrl = SeoHelper.ConvertToValidUrl(p.ReferenceTitle);
                rm.TUpdate(p);
                return RedirectToAction("Update", new { id = p.ReferenceID });
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