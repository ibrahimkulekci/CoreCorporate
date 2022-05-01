using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.Models;
using BusinessLayer.ValidationRules;
using CoreCorporate.Areas.AdminPanel.Models;
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
    public class TeamController : Controller
    {
        TeamManager tm = new TeamManager(new EfTeamRepository(new AppDbContext()));
        TeamValidator tv = new TeamValidator();

        public IActionResult Index(ListViewModel model)
        {
            if (model == null)
            {
                model = new ListViewModel();
                model.CurrentPage = 1;
                model.PageSize = 10;
                model.SortOn = nameof(EntityLayer.Concrete.Team.TeamCreatedDate);
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
                model.SortOn = nameof(EntityLayer.Concrete.Team.TeamCreatedDate);
            }

            if (string.IsNullOrEmpty(model.SortDirection))
            {
                model.SortDirection = "desc";
            }

            BaseResultListModel<EntityLayer.Concrete.Team> recordList = tm.GetAllByQuery(model);

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
        public IActionResult Add(Team p)
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
                return RedirectToAction("Index", "Team");
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
            var values = tm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(Team p)
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
                return RedirectToAction("Update", new { id = p.TeamID });
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
