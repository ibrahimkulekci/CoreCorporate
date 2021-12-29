using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ServiceCategory
    {
        [Key]
        public int ServiceCategoryID { get; set; }
        public string ServiceCategoryTitle { get; set; }
        public string ServiceCategoryUrl { get; set; }
        public string ServiceCategoryContent { get; set; }
        public bool ServiceCategoryStatus { get; set; }
        public DateTime ServiceCategoryCreatedDate { get; set; }
        public DateTime ServiceCategoryUpdatedDate { get; set; }
    }
}
