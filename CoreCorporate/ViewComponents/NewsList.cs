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
    public class NewsList:ViewComponent
    {
        NewsManager nm = new NewsManager(new EfNewsRepository(new AppDbContext()));

        public IViewComponentResult Invoke()
        {
            var values = nm.GetList().Take(3).Where(x=>x.NewsStatus==true);
            return View(values);
        }
    }
}
