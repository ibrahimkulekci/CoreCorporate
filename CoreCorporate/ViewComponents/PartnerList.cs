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
    public class PartnerList: ViewComponent
    {
        private readonly IPartnerService _partnerService;

        public PartnerList(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _partnerService.GetList().Where(x=>x.PartnerStatus==true).ToList();
            return View(values);
        }
    }
}
