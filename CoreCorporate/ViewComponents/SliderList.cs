using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.ViewComponents
{
    public class SliderList:ViewComponent
    {
        SliderService ss = new SliderService(new SliderRepository(new AppDbContext()));

        public IViewComponentResult Invoke()
        {
            var values = ss.GetList().OrderBy(x=>x.DisplayOrder).Where(x=>x.IsActive==true);
            return View(values);
        }
    }
}
