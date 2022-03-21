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
    public class MenuList: ViewComponent
    {
        MenuService ms = new MenuService(new EfMenuRepository(new AppDbContext()));

        public IViewComponentResult Invoke()
        {
            var values = ms.GetList().OrderBy(x=>x.MenuDisplayOrder).Where(y=>y.MenuStatus==true);
            return View(values);
        }
    }
}
