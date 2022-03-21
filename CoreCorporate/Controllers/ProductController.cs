using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IProductService _ps;
        private readonly IProductCategoryService _pcs;

        public ProductController(IProductService ps, IProductCategoryService pcs)
        {
            _ps = ps;
            _pcs = pcs;
        }

        public IActionResult Index()
        {
            var values = _ps.GetList().Where(x => x.ProductStatus == true).ToList();
            return View(values);
        }
        public IActionResult Details(string url)
        {
            var values = _ps.GetList().Where(x => x.ProductUrl == url).FirstOrDefault();
            return View(values);
        }
        public IActionResult Category(int id)
        {
            var categoryname = _pcs.TGetById(id);
            ViewBag.CategoryName = categoryname.ProductCategoryTitle;
            var values = _ps.GetList().Where(x => x.ProductCategoryID == id && x.ProductStatus==true).ToList();
            return View(values);
        }
    }
}
