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
    public class ServiceController : Controller
    {
        private readonly IServiceService _ss;

        public ServiceController(IServiceService ss)
        {
            _ss = ss;
        }

        public IActionResult Index()
        {
            var values = _ss.GetList().Where(x => x.ServiceStatus == true).ToList();
            return View(values);
        }
        public IActionResult Details(string url)
        {
            var values = _ss.GetList().Where(x => x.ServiceUrl == url).FirstOrDefault();
            return View(values);
        }
    }
}
