using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        SettingValidator sv = new SettingValidator();
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IActionResult Index()
        {
            var values = _settingService.GetList();
            return View(values);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Setting p)
        {
            ValidationResult results = sv.Validate(p);
            if (results.IsValid)
            {
                _settingService.TAdd(p);
                return RedirectToAction("Index", "Setting");
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
        public IActionResult Update(int id)
        {
            var values = _settingService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(Setting p)
        {
            ValidationResult results = sv.Validate(p);
            if (results.IsValid)
            {
                _settingService.TUpdate(p);
                return RedirectToAction("Update", new { id = p.Id });
            }
            return View();
        }
    }
}
