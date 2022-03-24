using BusinessLayer.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Models.Service
{
    public class ListViewModel: ServiceQueryModel
    {
        public List<BusinessLayer.Models.Service.ServiceWithDetail> DataList { get; set; }

        public long TotalRecordCount { get; set; }
        public long TotalPageCount { get { return TotalRecordCount == 0 ? 0 : (TotalRecordCount % PageSize == 0 ? TotalRecordCount / PageSize : (TotalRecordCount / PageSize) + 1); } }
        

        public string UIMessageContent { get; set; }
        public string UIMessageType { get; set; }
    }
}
