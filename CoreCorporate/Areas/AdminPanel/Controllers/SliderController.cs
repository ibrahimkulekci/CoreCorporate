using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin,Moderatör")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        SliderValidator sv = new SliderValidator();

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            var values = _sliderService.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Slider p)
        {
            ValidationResult results = sv.Validate(p);
            if (results.IsValid)
            {
                p.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                _sliderService.TAdd(p);
                return RedirectToAction("Index", "Slider");
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
            var values = _sliderService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(Slider p)
        {
            ValidationResult results = sv.Validate(p);
            if (results.IsValid)
            {
                p.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                _sliderService.TUpdate(p);
                return RedirectToAction("Update", new { id = p.ID });
            }
            return View();
        }
    }
}
