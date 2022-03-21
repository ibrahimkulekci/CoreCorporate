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
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        MenuValidator mv = new MenuValidator();

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public IActionResult Index()
        {
            var values = _menuService.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Menu p)
        {
            ValidationResult results = mv.Validate(p);
            if (results.IsValid)
            {
                p.MenuCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.MenuUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                _menuService.TAdd(p);
                return RedirectToAction("Index", "Menu");
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
            var values = _menuService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(Menu p)
        {
            ValidationResult results = mv.Validate(p);
            if (results.IsValid)
            {
                
                p.MenuUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                _menuService.TUpdate(p);

                return RedirectToAction("Update", new { id = p.MenuID });
            }
            return View();
        }
    }
}
