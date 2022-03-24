using BusinessLayer.Abstract;
using BusinessLayer.Common;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreCorporate.Areas.AdminPanel.Models.Page;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static BusinessLayer.Common.DataTableHelper;

namespace CoreCorporate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class PageController : Controller
    {
        //PageManager pm = new PageManager(new EfPageRepository(new AppDbContext()));
        private readonly IPageService _pageManager;
        PageValidator pv = new PageValidator();

        private string _connectionString;

        public PageController(IPageService pageManager, AppDbContext appDbContext, IConfiguration iconfiguration)
        {
            _pageManager = pageManager;
            _connectionString = iconfiguration.GetConnectionString("AppDbConnectionString");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PageList()
        {
            var values = _pageManager.GetList();
            return View(values);
        }
        [HttpPost]
        public JsonResult GetList([FromBody] DtParameters param)
        {
            var data = new PageIndexViewModel();
            using (SqlConnection con =new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd=new SqlCommand())
                {
                    cmd.CommandText = "[dbo].[Pages.GetList]";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("SearchVal", param.Search.Value);
                    cmd.Parameters.AddWithValue("Page", param.Start);
                    cmd.Parameters.AddWithValue("OrderBy", param.SortOrder);
                    cmd.Parameters.AddWithValue("PageSize", param.Length);
                    cmd.CommandTimeout = 120;
                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        PageIndexViewModel model = new PageIndexViewModel();
                        model.PageTitle = item["PageTitle"].ToString();
                        model.PageUrl = item["PageUrl"].ToString();
                        model.PageStatus = Convert.ToBoolean(item["PageStatus"].ToString());
                        model.PageCreatedDate = Convert.ToDateTime(item["PageCreatedDate"].ToString());
                        model.PageUpdatedDate = Convert.ToDateTime(item["PageUpdatedDate"].ToString());
                        data.DataTableList.Add(model);
                    }
                    data.Total = Convert.ToInt32(ds.Tables[1].Rows[0].ItemArray[0]);
                }
                con.Close();
            }
            var helper = new DtResult<PageIndexViewModel>()
            {
                Draw = param.Draw,
                Data = data.DataTableList.ToList(),
                RecordsFiltered = data.Total,
                RecordsTotal = data.Total
            };
            return Json(helper);
        }
        [HttpGet]
        public IActionResult PageAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PageAdd(Page p)
        {
            ValidationResult results = pv.Validate(p);
            if (results.IsValid)
            {
                p.PageCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PageStatus = p.PageStatus;
                p.PageUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PageUrl = SeoHelper.ConvertToValidUrl(p.PageTitle);
                _pageManager.TAdd(p);
                return RedirectToAction("PageList", "Page");
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
        public IActionResult PageUpdate(int id)
        {
            var values = _pageManager.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult PageUpdate(Page p)
        {
            ValidationResult results = pv.Validate(p);
            if (results.IsValid)
            {
                p.PageStatus = p.PageStatus;
                p.PageUpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.PageUrl = SeoHelper.ConvertToValidUrl(p.PageTitle);
                _pageManager.TUpdate(p);

                return RedirectToAction("PageUpdate", new { id = p.PageID });
            }
            return View();
        }
    }
}
