﻿using BusinessLayer.Common;
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
    public class ReferenceController : Controller
    {
        ReferenceManager rm = new ReferenceManager(new EfReferenceRepository(new AppDbContext()));
        ReferenceValidator rv = new ReferenceValidator();

        public IActionResult ReferenceList()
        {
            var values = rm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult ReferenceAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ReferenceAdd(Reference p)
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
                return RedirectToAction("ReferenceList", "Reference");
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
        public IActionResult ReferenceUpdate(int id)
        {
            var values = rm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult ReferenceUpdate(Reference p)
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
                return RedirectToAction("ReferenceUpdate", new { id = p.ReferenceID });
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