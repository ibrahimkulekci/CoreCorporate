using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.Models;
using BusinessLayer.ValidationRules;
using CoreCorporate.Areas.AdminPanel.Models.Partner;
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
    public class PartnerController : Controller
    {
        PartnerManager pm = new PartnerManager(new EfPartnerRepository(new AppDbContext()));
        PartnerValidator pv = new PartnerValidator();

        public IActionResult Index(ListViewModel model)
        {
            if (model == null)
            {
                model = new ListViewModel();
                model.CurrentPage = 1;
                model.PageSize = 10;
                model.SortOn = nameof(EntityLayer.Concrete.Partner.PartnerCreatedDate);
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
                model.SortOn = nameof(EntityLayer.Concrete.Partner.PartnerCreatedDate);
            }

            if (string.IsNullOrEmpty(model.SortDirection))
            {
                model.SortDirection = "desc";
            }

            BaseResultListModel<EntityLayer.Concrete.Partner> recordList = pm.GetAllByQuery(model);

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
        public IActionResult Add(Partner p)
        {
            ValidationResult results = pv.Validate(p);
            if (results.IsValid)
            {
                if (p.PartnerImageFile != null)
                {
                    var extension = Path.GetExtension(p.PartnerImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.PartnerTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/PartnerImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.PartnerImageFile.CopyTo(stream);
                    p.PartnerImage = newImageName;
                }
                else
                {
                    p.PartnerImage = "nullimage.jpg";
                }
                p.PartnerCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PartnerUpdateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PartnerUrl = SeoHelper.ConvertToValidUrl(p.PartnerTitle);
                pm.TAdd(p);
                return RedirectToAction("Index", "Partner");
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
            var values = pm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(Partner p)
        {
            ValidationResult results = pv.Validate(p);
            if (results.IsValid)
            {
                if (p.PartnerImageFile != null)
                {
                    var extension = Path.GetExtension(p.PartnerImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.PartnerTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/PartnerImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.PartnerImageFile.CopyTo(stream);
                    p.PartnerImage = newImageName;
                }
                p.PartnerUpdateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PartnerUrl = SeoHelper.ConvertToValidUrl(p.PartnerTitle);
                pm.TUpdate(p);
                return RedirectToAction("Update", new { id = p.PartnerID });
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
