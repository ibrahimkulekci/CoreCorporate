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
    public class TeamController : Controller
    {
        TeamManager tm = new TeamManager(new EfTeamRepository(new AppDbContext()));
        TeamValidator tv = new TeamValidator();

        public IActionResult TeamList()
        {
            var values = tm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult TeamAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TeamAdd(Team p)
        {
            ValidationResult results = tv.Validate(p);
            if (results.IsValid)
            {
                if (p.TeamImageFile != null)
                {
                    var extension = Path.GetExtension(p.TeamImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.TeamName) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/TeamImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.TeamImageFile.CopyTo(stream);
                    p.TeamImage = newImageName;
                }
                else
                {
                    p.TeamImage = "nullimage.jpg";
                }
                p.TeamCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.TeamUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.TeamUrl = SeoHelper.ConvertToValidUrl(p.TeamName);
                tm.TAdd(p);
                return RedirectToAction("TeamList", "Team");
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
        public IActionResult TeamUpdate(int id)
        {
            var values = tm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult TeamUpdate(Team p)
        {
            ValidationResult results = tv.Validate(p);
            if (results.IsValid)
            {
                if (p.TeamImageFile != null)
                {
                    var extension = Path.GetExtension(p.TeamImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.TeamName) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/TeamImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.TeamImageFile.CopyTo(stream);
                    p.TeamImage = newImageName;
                }
                p.TeamUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.TeamUrl = SeoHelper.ConvertToValidUrl(p.TeamName);
                tm.TUpdate(p);
                return RedirectToAction("TeamUpdate", new { id = p.TeamID });
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
