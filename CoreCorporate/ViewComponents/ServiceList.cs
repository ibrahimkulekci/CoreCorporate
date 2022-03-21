using BusinessLayer.Abstract;
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
    public class ServiceList: ViewComponent
    {
        ServiceManager sm = new ServiceManager(new EfServiceRepository(new AppDbContext()));

        public IViewComponentResult Invoke()
        {
            var values = sm.GetList().Where(x=>x.ServiceStatus==true);
            return View(values);
        }
    }
}
