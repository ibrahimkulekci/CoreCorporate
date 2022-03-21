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

namespace CoreCorporate.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ISettingService _settingService;

        ContactValidator _cv = new ContactValidator();

        public ContactController(IContactService contactService, ISettingService settingService)
        {
            _contactService = contactService;
            _settingService = settingService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Contact p)
        {
            ValidationResult results = _cv.Validate(p);
            if (results.IsValid)
            {
                p.GalleryCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.GalleryUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                _contactService.TAdd(p);
                TempData["basari"] = "Mesajınız başarıyla iletilmiştir. En kısa zamanda E-posta veya Telefon ile iletişim kurulacaktır.";
                return RedirectToAction("Index", "Contact");
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
