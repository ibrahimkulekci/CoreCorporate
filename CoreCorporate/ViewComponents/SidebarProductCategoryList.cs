using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.ViewComponents
{
    public class SidebarProductCategoryList:ViewComponent
    {
        private readonly IProductCategoryService _pcs;

        public SidebarProductCategoryList(IProductCategoryService pcs)
        {
            _pcs = pcs;
        }

        public IViewComponentResult Invoke()
        {
            var values = _pcs.GetList().Where(y => y.ProductCategoryStatus == true);
            return View(values);
        }

    }
}
