using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Page
    {
        [Key]
        public int PageID { get; set; }
        public string PageTitle { get; set; }
        public string PageUrl { get; set; }
        public string PageContent { get; set; }
        public bool PageStatus { get; set; }
        public DateTime PageCreatedDate { get; set; }
        public DateTime PageUpdatedDate { get; set; }
    }
}
