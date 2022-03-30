using BusinessLayer.Abstract;
using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.Models;
using BusinessLayer.ValidationRules;
using CoreCorporate.Areas.AdminPanel.Models.Gallery;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class GalleryController : Controller
    {
        //GalleryManager gm = new GalleryManager(new GalleryRepository());
        GalleryValidator gv = new GalleryValidator();

        private readonly AppDbContext _appDbContext; //= new AppDbContext();

        private readonly IGalleryService _gm;
        private readonly IGalleryImageService _galleryImageService;

        public GalleryController(AppDbContext appDbContext, IGalleryService gm, IGalleryImageService galleryImageService)
        {
            //_appDbContext = new AppDbContext();

            _appDbContext = appDbContext;

            _gm = gm;
            _galleryImageService = galleryImageService;
        }

        public IActionResult Index(ListViewModel model)
        {
            if (model == null)
            {
                model = new ListViewModel();
                model.CurrentPage = 1;
                model.PageSize = 10;
                model.SortOn = nameof(EntityLayer.Concrete.Gallery.GalleryCreatedDate);
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
                model.SortOn = nameof(EntityLayer.Concrete.Gallery.GalleryCreatedDate);
            }

            if (string.IsNullOrEmpty(model.SortDirection))
            {
                model.SortDirection = "desc";
            }

            BaseResultListModel<EntityLayer.Concrete.Gallery> recordList = _gm.GetAllByQuery(model);

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
        public IActionResult Add(Gallery p)
        {
            ValidationResult results = gv.Validate(p);
            if (results.IsValid)
            {
                /*if(p.GalleryImageFile != null)
                {
                    foreach (var item in p.GalleryImageFile)
                    {
                        p.GalleryImageFile.Count();
                        if (item.Length > 0)
                        {
                            
                            var extension = Path.GetExtension(item.FileName);
                            var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.GalleryTitle) + extension;
                            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/GalleryImages/", newImageName);
                            var stream = new FileStream(location, FileMode.Create);
                            item.CopyTo(stream);
                        }
                    }
                }*/
                if (p.GalleryImageFile != null)
                {
                    var extension = Path.GetExtension(p.GalleryImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.GalleryTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/GalleryImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.GalleryImageFile.CopyTo(stream);
                    p.GalleryImage = newImageName;
                }
                else
                {
                    p.GalleryImage = "nullimage.jpg";
                }
                p.GalleryCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.GalleryUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.GalleryUrl = SeoHelper.ConvertToValidUrl(p.GalleryTitle);
                _gm.TAdd(p);
                return RedirectToAction("Index", "Gallery");
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
            var values = _gm.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Update(Gallery p)
        {
            ValidationResult results = gv.Validate(p);
            if (results.IsValid)
            {
                if (p.GalleryImageFile != null)
                {
                    var extension = Path.GetExtension(p.GalleryImageFile.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(p.GalleryTitle) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ServiceImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.GalleryImageFile.CopyTo(stream);
                    p.GalleryImage = newImageName;
                }
                p.GalleryUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.GalleryUrl = SeoHelper.ConvertToValidUrl(p.GalleryTitle);
                _gm.TUpdate(p);
                return RedirectToAction("Update", new { id = p.GalleryID });
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
        public IActionResult GalleryImageAdd(int id)
        {
            var existGallery = _gm.TGetById(id);
            //return View(values);

            GalleryImageAddViewModel model = new GalleryImageAddViewModel();

            model.GalleryId = existGallery.GalleryID;
            model.GalleryTitle = existGallery.GalleryTitle;

            var existImageList = _galleryImageService.GetAllByGalleryId(existGallery.GalleryID);

            model.GalleryImageList.AddRange(existImageList);

            return View(model);
        }

        [HttpPost]
        public IActionResult GalleryImageAdd(GalleryImageAddViewModel model)
        {
            try
            {
                // dosya kaydetme adımları olacak
                // dbye kaydetme adımları olacak
                for (int i = 0; i < model.FileList.Count; i++)
                {
                    var extension = Path.GetExtension(model.FileList[i].FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(model.GalleryTitle + "-" + i) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/GalleryImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    model.FileList[i].CopyTo(stream);
                    model.GalleryImageList.Add(new GalleryImage { GalleryId = model.GalleryId, ImageUrl = newImageName, DisplayOrder = 0 });

                    _galleryImageService.TAdd(model.GalleryImageList[i]);

                }



                ViewBag.Message = "Dosya başarıyla kaydedildi.";

                var existImageList = _galleryImageService.GetAllByGalleryId(model.GalleryId);
                model.GalleryImageList.AddRange(existImageList);

            }
            catch (Exception exception)
            {
                ViewBag.ErrorMessage = "Dosya kaydedilemedi. Hata detayı: " + exception.Message;
            }
            return RedirectToAction("Index", "Gallery");
        }

        [HttpPost]
        //public IActionResult GalleryImageUpdate(int imageId, int displayOrder, string submitType)
        public IActionResult GalleryImageUpdate(List<GalleryImage> GalleryImageList, string SubmitType, int GalleryId)
        {
            if (SubmitType.StartsWith("btnUpdate"))
            {
                // update işlemi yapılacak
                int selectedGalleryImageId = Convert.ToInt32(SubmitType.Replace("btnUpdate", ""));

                int newDisplayOrder = GalleryImageList.Where(r => r.GalleryImageId == selectedGalleryImageId).FirstOrDefault().DisplayOrder;

                var existRecord = _galleryImageService.TGetById(selectedGalleryImageId);
                existRecord.DisplayOrder = newDisplayOrder;

                _galleryImageService.TUpdate(existRecord);
            }
            else if (SubmitType.StartsWith("btnDelete"))
            {
                // delete işlemi olacak
            }

            
            return RedirectToAction("GalleryImageAdd", "Gallery", new { id = GalleryId });

        }


    }
}
