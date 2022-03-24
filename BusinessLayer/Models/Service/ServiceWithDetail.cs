using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Service
{
    public class ServiceWithDetail: EntityLayer.Concrete.Service
    {
        //ServiceCategory property'leri.
        public string ServiceCategoryTitle { get; set; }
        public string ServiceCategoryUrl { get; set; }
        public string ServiceCategoryContent { get; set; }
        public bool ServiceCategoryStatus { get; set; }

    }
}
