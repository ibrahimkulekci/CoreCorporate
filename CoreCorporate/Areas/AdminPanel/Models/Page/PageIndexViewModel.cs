using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Models.Page
{
    public class PageIndexViewModel
    {
        public PageIndexViewModel()
        {
            DataTableList = new List<PageIndexViewModel>();
        }

        public int PageID { get; set; }
        public string PageTitle { get; set; }
        public string PageUrl { get; set; }
        public string PageContent { get; set; }
        public bool PageStatus { get; set; }
        public DateTime PageCreatedDate { get; set; }
        public DateTime PageUpdatedDate { get; set; }
        public int Total { get; set; }
        public List<PageIndexViewModel> DataTableList { get; set; }
    }
}
