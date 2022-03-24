using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Service
{
    public class ServiceQueryModel:BaseQueryModel
    {
        //fiters
        public long? Filter_ServiceID { get; set; }
        public long? Filter_ServiceCategoryID { get; set; }
        public bool? Filter_ServiceStatus { get; set; }
        public string? Filter_Search { get; set; }

        public DateTime? Filter_PublishDateTime_Begin { get; set; }
        public DateTime? Filter_PublishDateTime_End { get; set; }
    }
}
